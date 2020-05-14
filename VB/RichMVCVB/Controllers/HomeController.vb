Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        ViewBag.Document = Convert.ToBase64String(System.IO.File.ReadAllBytes(Server.MapPath("~/Docs/template.docx")))
        Return View()
    End Function

    <HttpPost>
    Public Function ExportDocument(ByVal base64 As String, ByVal fileName As String, ByVal format As Integer, ByVal reason As String) As ActionResult
        Dim fileContent As Byte() = Convert.FromBase64String(base64)
        System.IO.File.WriteAllBytes(Server.MapPath($"~/Docs/{fileName}.docx"), fileContent)
        Return New EmptyResult()
    End Function
End Class