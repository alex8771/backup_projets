<%-- 
    Document   : home
    Created on : 12 nov. 2019, 17:05:03
    Author     : yacin
--%>

<jsp:include page="header.jsp">
    <jsp:param name="title" value="Accueil"/>
</jsp:include>
<%@ include file="navbar.jsp"%>
<div class="jumbotron jumbotron-fluid">
    <div class="container">
        <h1 class="display-4">Bienvenue <b><%= session.getAttribute("fullname")%></b></h1>
        <p class="lead">
            Ceci est la page principale pour <b><%= session.getAttribute("profile")%></b>.
        </p>
    </div>
    <%@ include file="footer.jsp"%>