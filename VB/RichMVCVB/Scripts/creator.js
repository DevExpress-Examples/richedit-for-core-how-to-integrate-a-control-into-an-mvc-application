/// <reference path="../node_modules/devexpress-richedit/dist/dx.richedit.d.ts" />

function createRichEdit(richEditContainer, initialOptions) {
    /** @type {DevExpress.RichEdit.Options} */
    var options = DevExpress.RichEdit.createOptions();
    customizeRibbon(options);
    options.exportUrl = initialOptions.exportUrl;
    options.confirmOnLosingChanges.enabled = false;

    var elem = document.createElement('div');
    richEditContainer.append(elem);

    /** @type {DevExpress.RichEdit.RichEdit} */
    var rich = DevExpress.RichEdit.create(elem, options);
    rich.openDocument(initialOptions.document, 'fileName', DevExpress.RichEdit.DocumentFormat.OpenXml, function () {
        rich.document.insertText(rich.document.length, 'Some text');
    });
    return rich;
}

/**
 * @param {DevExpress.RichEdit.Options} options
 */
function customizeRibbon(options) {
    options.ribbon.removeTab(DevExpress.RichEdit.RibbonTabType.MailMerge);
    options.ribbon.removeTab(DevExpress.RichEdit.RibbonTabType.References);
    options.ribbon.getTab(DevExpress.RichEdit.RibbonTabType.File)
        .removeItem(DevExpress.RichEdit.FileTabItemId.OpenDocument);
    options.ribbon.getTab(DevExpress.RichEdit.RibbonTabType.View)
        .removeItem(DevExpress.RichEdit.ViewTabItemId.ToggleShowHorizontalRuler);
}

/**
 * @param {DevExpress.RichEdit.RichEdit} rich
 * @param {string} url
 */
function setDataSource(rich, url) {
    rich.loadingPanel.show();
    $.get(url, function (data) {
        rich.mailMergeOptions.setDataSource(data, function () {
            rich.loadingPanel.hide();
        });
    }, "json");
}

/**
 * @param {DevExpress.RichEdit.RichEdit} rich
  */
function mailMerge(rich) {
    if (!rich.mailMergeOptions.getDataSource()) {
        alert('No data source');
        return;
    }

    rich.loadingPanel.show();
    rich.mailMerge(function (documentAsBlob) {
        blobToBase64(documentAsBlob, function (documentAsBase64) {
            rich.openDocument(documentAsBase64, 'MergedDocument', DevExpress.RichEdit.DocumentFormat.OpenXml, function () {
                rich.loadingPanel.hide();
            });
        });
    }, DevExpress.RichEdit.MergeMode.NewParagraph, DevExpress.RichEdit.DocumentFormat.OpenXml);
}

function blobToBase64(blob, callback) {
    var reader = new FileReader();
    reader.readAsDataURL(blob);
    reader.onloadend = function () {
        var base64data = reader.result;
        callback(base64data);
    }
}
/**
 * @param {DevExpress.RichEdit.RichEdit} rich
  */
function appendMergeFields(rich) {
    var position = rich.selection.active;
    var sd = rich.selection.activeSubDocument;

    function insertField(name) {
        var field = sd.fields.createMergeField(position, name);
        field.update();
        position = field.interval.end;
    }

    rich.beginUpdate();
    rich.history.beginTransaction();
    position = sd.insertParagraph(position).interval.end;
    position = sd.insertText(position, 'FirstName: ').end;
    insertField('FirstName');
    position = sd.insertText(position, ', Id: ').end;
    insertField('Id');
    position = sd.insertParagraph(position).interval.end;
    rich.history.endTransaction();
    rich.endUpdate();
    rich.focus();
}

