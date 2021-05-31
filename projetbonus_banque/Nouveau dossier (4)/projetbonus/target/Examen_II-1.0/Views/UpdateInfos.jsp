<%-- 
    Document   : UpdateInfos
    Created on : 2019-12-23, 16:15:44
    Author     : Alexandre
--%>

<%@page import="com.mycompany.examen_ii.dto.UserDTO"%>
<%@page import="java.util.ArrayList"%>
<%@page import="java.util.List"%>
<jsp:include page="header.jsp">
    <jsp:param name="title" value="Accueil"/>
</jsp:include>
<%@ include file="navbar.jsp"%>
<%--<%@ include file="navbar.jsp"%>--%>
<div class="jumbotron jumbotron-fluid">
    <div class="container">
        <h2>Mettre à jour le mot de passe</h2>
        <form action="${pageContext.request.contextPath}/UpdateInfo" method="post">
            <div class="form-group row">
                <div class="col">
                    <input required type="password" class="form-control" name="new_data_password" placeholder="Entrez votre nouveau mot de passe">
                </div>
                <div class="col">
                    <button class="btn btn-primary float-right" type="submit" style="width: 200px;">Modifier mot de passe</button>
                </div>
            </div>  
        </form>

        <form action="${pageContext.request.contextPath}/UpdateEmail" method="post">
            <div class="form-group row">
                <div class="col">
                    <input required type="text" class="form-control" name="new_data_email" placeholder="Entrez votre nouveau courriel">
                </div>
                <div class="col">
                    <button class="btn btn-primary float-right" type="submit" style="width: 200px;">Modifier votre courriel</button>
                </div>
            </div>  
        </form>

        <form action="${pageContext.request.contextPath}/UpdateAdresse" method="post">
            <div class="form-group row">
                <div class="col">
                    <input required type="text" class="form-control" name="new_data_adresse" placeholder="Entrez votre nouvelle adresse">
                </div>
                <div class="col">
                    <button class="btn btn-primary float-right" type="submit" style="width: 200px;">Modifier adresse</button>
                </div>
            </div>  
        </form>

        <% if (request.getAttribute("erreurMotdePasse") == "false") { %>
        <div class="alert alert-success alert-dismissible fade show" role="alert">
            <b>Info :</b> le mot de passe a été mis à jour !
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <% } else if (request.getAttribute("erreurMotdePasse") == "true") { %>
        <div class="alert alert-danger alert-dismissible fade show" role="alert">
            <b>Erreur :</b> le mot de passe n'a pas été mis à jour !
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <% }%>

        <% if (request.getAttribute("erreurEmail") == "false") { %>
        <div class="alert alert-success alert-dismissible fade show" role="alert">
            <b>Info :</b> l'email a été mis à jour !
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <% } else if (request.getAttribute("erreurEmail") == "true") { %>
        <div class="alert alert-danger alert-dismissible fade show" role="alert">
            <b>Erreur :</b> l'email n'a pas été mis à jour !
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <% }%>

        <% if (request.getAttribute("erreurAdresse") == "false") { %>
        <div class="alert alert-success alert-dismissible fade show" role="alert">
            <b>Info :</b> l'adresse a été mis à jour !
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <% } else if (request.getAttribute("erreur") == "true") { %>
        <div class="alert alert-danger alert-dismissible fade show" role="alert">
            <b>Erreur :</b> l'adresse n'a pas été mis à jour !
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <% }%>
    </div>
    <%@ include file="footer.jsp"%>
