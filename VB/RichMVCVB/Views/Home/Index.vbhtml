@Code
    ViewData("Title") = "Home Page"
End Code

@Scripts.Render("~/Scripts/creator.js")

<button onclick="setDataSource(window.rich, '@Url.Action("GetDataSource", "Home", Nothing, Request.Url.Scheme)')">setDataSource</button>
<button onclick="appendMergeFields(window.rich)">addMergeFields</button>
<button onclick="mailMerge(window.rich)">mailMerge</button>
<div id='rich-container' style="width: 100%; height: 900px"></div>

<script>
    $(document).ready(function () {
        const rich = createRichEdit($("#rich-container"), {
            exportUrl: '@Url.Action("ExportDocument", "Home", Nothing, Request.Url.Scheme)',
            document: "@ViewBag.Document",
        });
        window.rich = rich;
    });
</script>