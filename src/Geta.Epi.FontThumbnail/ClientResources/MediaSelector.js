define([
// dojo
"dojo/_base/array",
"dojo/_base/declare",
"dojo/_base/lang",
"dojo/dom-construct",
"dojo/when",
"dojo/promise/all",

// episerver mixins
"epi-cms/_ContentContextMixin",

// episerver shell
"epi/dependency",
"epi/shell/TypeDescriptorManager",

// episerver
"epi-cms/widget/ContentSelector",
"epi-cms/widget/FilesUploadDropZone",
"epi-cms/widget/UploadUtil",
"epi-cms/widget/viewmodel/MultipleFileUploadViewModel",
"epi-cms/widget/MultipleFileUpload",
"epi-cms/ApplicationSettings",
"epi-cms/core/ContentReference",

// template
"dojo/text!./templates/AssetsDropZone.html",
"xstyle/css!./style.css",

// Resources
"epi/i18n!epi/cms/nls/episerver.cms.widget.hierachicallist"],
function (

// dojo
array,
declare,
lang,
domConstruct,
when,
promiseAll,

// episerver mixins
_ContentContextMixin,

// episerver shell
dependency,
TypeDescriptorManager,

// episerver
ContentSelector,
DropZone,
UploadUtil,
MultipleFileUploadViewModel,
MultipleFileUpload,
ApplicationSettings,
ContentReference,

//template
dropZoneTemplate,
css,

//resources
res) {
	return declare("geta-epi-mediareferenceselector/MediaSelector", [ContentSelector, _ContentContextMixin], {
	res: res,
	droppableContainer: null,
	currentContent: null,
	inCreateMode: false,

	// store related properties
	store: null,
	storeKey: "epi.cms.content.light",
	listQuery: null,

	// upload related properties
	upload_target: null,
	upload_createAsLocalAsset: true,


	postMixInProperties: function() {
		this.inherited(arguments);

		this.store = this.store || dependency.resolve("epi.storeregistry").get(this.storeKey);

		// load the current content and context
		when(promiseAll([this.getCurrentContent(), this.getCurrentContext()]), lang.hitch(this, function (result) {

			var content = result[0],
			currentContext = result.length > 1 ? result[1] : null;

			// set the currentcontent
			this.currentContent = content;

			// determine if current content is in "create"-mode
			this.inCreateMode = currentContext && currentContext.currentMode && currentContext.currentMode === "create";

			this.upload_target = this.currentContent.contentLink;
			this.upload_createAsLocalAsset = true;

			// if we are creating content, since the assets folder is not yet created we use the global assets folder instead.
			if (this.inCreateMode) {
				this.upload_target = ApplicationSettings.globalAssetsFolder;
				this.upload_createAsLocalAsset = false;
			}

		}));
	},

	buildRendering: function() {
		this.inherited(arguments);
		this._setupDropZone();
	},

	_setupDropZone: function() {
		this.inherited(arguments);

		// create a container for the additional dropzone (mainly for css reasons)
		this.droppableContainer = domConstruct.create("div",
			{ className: "custom-dropzone" }
		);

		// use the same html-template and resources that is used in the ordinary
		// hierarchical list
		this.own(this._dropZone = new DropZone({
			templateString: dropZoneTemplate,
			res: res,
			outsideDomNode: this.droppableContainer
		}));
		domConstruct.place(this._dropZone.domNode, this.droppableContainer, "last");

		// create event listener for the onDrop-event
		this.connect(this._dropZone, "onDrop", this._onDrop);

		// append the container
		this.domNode.appendChild(this.droppableContainer);

	},

	_onDrop: function(evt, fileList) {
		// filters out empty files and files without type
		fileList = UploadUtil.filterFileOnly(fileList);

		// ignore empty filelists (folders)
		if (!fileList || fileList.length <= 0) {
			return;
		}
		// upload the file, make sure to only upload the first one since we 
		// only support one file at a time
		this.upload(fileList[0], this.upload_target, this.upload_createAsLocalAsset);
	},

	upload: function(file, targetId, createAsLocalAsset) {
		var uploader = new MultipleFileUpload({
			model: new MultipleFileUploadViewModel({})
		});

		// event triggered when the upload is completed
		uploader.on("uploadComplete", lang.hitch(this, function(uploadedFiles) {
			if (!uploadedFiles || uploadedFiles.length <= 0) {
				return;
			}

			var uploaded = uploadedFiles[0];

			// if a file has been uploaded to a newly created assetsfolder, 
			// we need to refresh the content to get the correct assetsfolder link.
			var targetContentLinkWithoutVersion =
				new ContentReference(this.upload_target).createVersionUnspecificReference().toString();

			when(this.store.refresh(targetContentLinkWithoutVersion), lang.hitch(this, function (refreshedContent) {
				// used for refreshing the treeview and also find the dropped file
				// set the list query to be able to query children of the current 
				// assets folder (needed to find out the contentLink for the uploaded item)
				this.listQuery = {
					referenceId: refreshedContent.assetsFolderLink,
					query: "getchildren",
					allLanguages: true,
					typeIdentifiers: []
				};

				if (this.inCreateMode) {
					this.listQuery.referenceId = this.upload_target;
				}

				// query the store for the children to assetsfolder
				when(this.store.query(this.listQuery), lang.hitch(this,
					function (contentList) {
						// filter out only files with same name as uploaded file
						var createdContent = array.filter(contentList, function (content) {
							return content.name === uploaded.fileName;
						});

						if (!createdContent || createdContent.length <= 0) {
							return;
						}

						// select first match
						var created = createdContent[0];

						// only set the value if the uploaded type is allowed
						if (this._isContentAllowed(created.typeIdentifier)) {
						    this.focus();
						    this.set('value', created.contentLink);
						    this.blur();
						}
					}
				));
			}));
		}));

		// upload the file to the assets folder
		uploader.set("uploadDirectory", targetId);
		uploader.set("createAsLocalAsset", createAsLocalAsset);
		uploader.upload([file]);
	},

	_isContentAllowed: function(contentTypeIdentifier) {
		var acceptedTypes = TypeDescriptorManager.getValidAcceptedTypes(
			[contentTypeIdentifier],
			this.allowedTypes,
			this.restrictedTypes);

		return !!acceptedTypes.length;
	}
});
});