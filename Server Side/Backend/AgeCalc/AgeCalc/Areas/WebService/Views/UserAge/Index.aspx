<%@ Page Language="C#"  
  Inherits="System.Web.Mvc.ViewPage<List<AgeCalc.Areas.WebService.Models.UserAge>>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Index</title>
</head>
<body>
    <div>
    
<% foreach (var m in ViewData.Model)
   { %>

    Title: <%= m.FirstName %>
    <br />
    Director: <%= m.LastName %>
    <br />
    <%= Html.ActionLink("Edit", "Edit", new { id = m.UserId })%>
    <%= Html.ActionLink("Delete", "Delete", new { id = m.UserId })%>
       
        <hr />
<% } %>

<%= Html.ActionLink("Add Movie", "Add") %>
    
    </div>
</body>
</html>