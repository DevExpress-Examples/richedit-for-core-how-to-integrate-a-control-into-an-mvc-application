# RichEdit for Core - How to integrate a control into an MVC application

This example illustrates a possible way of integrating a client part of ASP.NET Core Rich Edit into an MVC application. This can be done as follows:
1. Right-click the application's name in the **Solution Explorer** and select **Add | Add New Item**. In the invoked **Add New Item** dialog, select the **Installed | Visual C# | ASP.NET Core | Web** category and the **npm Configuration File** item template. Click **Add**.
This adds the **package.json** file to the project. Open this file and add the following dependencies:
```json
{
  "version": "1.0.0",
  "name": "asp.net",
  "private": true,
  "dependencies": {
    "devextreme": "20.1.3",
    "devexpress-richedit": "20.1.3"
  }
}
```

2. Create a RichEdit bundle using recommendations from this help topic: [Create a RichEdit Bundle](https://docs.devexpress.com/AspNetCore/401721/office-inspired-controls/get-started/richedit-bundle#create-a-richedit-bundle) 
3. Register the necessary script and styles in the default layout view (usually, it is "_Layout.cshtml") **before the DevExpress script registration**:

```razor
@Scripts.Render("~/node_modules/devexpress-richedit/dist/custom/dx.richedit.min.js")
@Styles.Render("~/node_modules/devexpress-richedit/dist/dx.richedit.css")
@Styles.Render("~/node_modules/devextreme/dist/css/dx.common.css")
@Styles.Render("~/node_modules/devextreme/dist/css/dx.light.compact.css")
```

You may need to install the "Microsoft.AspNet.Web.Optimization" package for this.

4. Create an action method for the document export:

```cs
[HttpPost]
public ActionResult ExportDocument(string base64, string fileName, int format, string reason)
{
	byte[] fileContent = Convert.FromBase64String(base64);
	System.IO.File.WriteAllBytes(Server.MapPath($"~/Docs/{fileName}.docx"), fileContent);
	return new EmptyResult();
}
```

```vb
<HttpPost>
Public Function ExportDocument(ByVal base64 As String, ByVal fileName As String, ByVal format As Integer, ByVal reason As String) As ActionResult
	Dim fileContent As Byte() = Convert.FromBase64String(base64)
	System.IO.File.WriteAllBytes(Server.MapPath($"~/Docs/{fileName}.docx"), fileContent)
	Return New EmptyResult()
End Function
```

5. If you wish to open a document on the RichEdit first load, save the document path to ViewBag/ViewData to pass it to RichEdit's view:

```cs
ViewBag.Document = Convert.ToBase64String(System.IO.File.ReadAllBytes(Server.MapPath("~/Docs/template.docx")));
```
```vb
ViewBag.Document = Convert.ToBase64String(System.IO.File.ReadAllBytes(Server.MapPath("~/Docs/template.docx")))
```

6. Use the static **DevExpress.RichEdit.createOptions** and **DevExpress.RichEdit.create** methods to create control options and the control itself respectively. To simplify this process, we created the "creator.js" file located at the "~/Scripts" folder.
It is enough to call the **createRichEdit** method located in this file:

```razor
<script>
    $(document).ready(function () {
        const rich = createRichEdit($("#rich-container"), {
            exportUrl: '@Url.Action("ExportDocument", "Home", Nothing, Request.Url.Scheme)',
            document: "@ViewBag.Document",
        });
        window.rich = rich;
    });
</script>
```

<!-- default file list -->
*Files to look at*:

* [Index.cshtml](./CS/RichMVC/Views/Home/Index.cshtml) (VB: [Index.vbhtml](./VB/RichMVCVB/Views/Home/Index.vbhtml))
* [_Layout.cshtml](./CS/RichMVC/Views/Shared/_Layout.cshtml) (VB: [_Layout.vbhtml](./VB/RichMVCVB/Views/Shared/_Layout.vbhtml))
* [creator.js](./CS/RichMVC/Scripts/creator.js)
* [HomeController.cs](./CS/RichMVC/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/RichMVCVB/Controllers/HomeController.vb))
<!-- default file list end -->
