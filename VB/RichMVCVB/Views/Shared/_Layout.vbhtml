<!DOCTYPE html>

<html>
<head>
    <meta charset="UTF-8" />
    <title>@ViewData("Title")</title>

    @Scripts.Render("~/node_modules/devexpress-richedit/dist/custom/dx.richedit.min.js")
    @Styles.Render("~/node_modules/devexpress-richedit/dist/dx.richedit.css")
    @Styles.Render("~/node_modules/devextreme/dist/css/dx.common.css")
    @Styles.Render("~/node_modules/devextreme/dist/css/dx.light.compact.css")

    @Html.DevExpress().GetStyleSheets(New StyleSheet With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout}, New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})
    @Html.DevExpress().GetScripts(New Script With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout}, New Script With {.ExtensionSuite = ExtensionSuite.Editors})
</head>

<body>
    @RenderBody()
</body>
</html>