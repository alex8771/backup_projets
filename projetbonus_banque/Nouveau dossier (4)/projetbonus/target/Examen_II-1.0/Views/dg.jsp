<%-- 
    Document   : dg
    Created on : 2019-12-06, 15:20:14
    Author     : 1896271
--%>

<%@page import="com.mycompany.examen_ii.dto.UserDTO"%>
<%@page import="java.util.ArrayList"%>
<%@page import="java.util.List"%>
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
    <div id="temps">
        <div class="form-inline d-flex justify-content-around">
            <div>
                <form action="${pageContext.request.contextPath}/fraisTransfert" method="post">
                    <label>Frais pour tansfert d'argent</label>
                    <input type="number" value="0" min="0" step="0.01" id="Transfertcontrole" name="data_transfert">
                    <button class="btn btn-success" type="submit">Modifier</button>
                </form>
            </div>
            <div>
                <form action="${pageContext.request.contextPath}/fraisRetrait" method="post">
                    <label>Frais pour retrait d'argent</label>
                    <input type="number" value="0" min="0" step="0.01" id="Retraitcontrole" name="data_retrait">
                    <button class="btn btn-success" type="submit">Modifier</button>
                </form>
            </div>
        </div> 
    </div>   
    <%@ include file="footer.jsp"%>