Public Class HomeController
    Inherits System.Web.Mvc.Controller
    Private Const documentFolderPath = "~/Docs/"
    Function Index() As ActionResult
        ViewBag.Document = Convert.ToBase64String(System.IO.File.ReadAllBytes(Server.MapPath($"{documentFolderPath}template.docx")))
        Return View()
    End Function

    <HttpPost>
    Public Function ExportDocument(ByVal base64 As String, ByVal fileName As String, ByVal format As Integer, ByVal reason As String) As ActionResult
        Dim fileContent As Byte() = Convert.FromBase64String(base64)
        System.IO.File.WriteAllBytes(Server.MapPath($"{documentFolderPath}{fileName}.{GetExtension(format)}"), fileContent)
        Return New EmptyResult()
    End Function

    Private Shared Function GetExtension(ByVal format As Integer) As String
        Select Case format
            Case 4
                Return "docx"
            Case 3
                Return "rtf"
            Case 1
                Return "txt"
        End Select
        Return "docx"
    End Function

    <HttpGet>
    Public Function GetDataSource() As JsonResult
        Dim id As Integer = 0
        Dim jsondata = {"John", "Piter", "Mark"}.[Select](Function(name) New Hashtable() From {
            {"FirstName", name},
            {"Id", Math.Min(System.Threading.Interlocked.Increment(id), id - 1)}
        }).ToList()
        Return Json(jsondata, JsonRequestBehavior.AllowGet)
    End Function
End Class