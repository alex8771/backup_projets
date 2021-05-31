<%-- 
    Document   : profil
    Created on : 2019-12-21, 12:29:25
    Author     : Alexandre
--%>

<%@ include file="header.jsp"%>
<%@ include file="navbar.jsp"%>
<div id="myGlobalContainer" class="d-flex align-items-center justify-content-center">
    <div class="card" id="connect">
        <h5 class="card-header">Profil</h5>
        <div class="card-body">
            <form action="${pageContext.request.contextPath}/profil" method="post">
                <div class="form-group row">
                    <div class="col">
                        <label>Nom:</label>
                    </div>
                    <div class="col">
                        <input type="text" class="form-control" name="data_name" value="<%= session.getAttribute("nom")%>" disabled="" >
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Prénom:</label>
                    </div>
                    <div class="col">
                        <input type="text" class="form-control" name="data_prenom" value="<%= session.getAttribute("fullname")%>" disabled="" >
                    </div>
                </div> 
                <div class="form-group row">
                    <div class="col">
                        <label>Émail</label>
                    </div>
                    <div class="col">
                        <input type="text" class="form-control"  name="data_email" value="<%= session.getAttribute("email")%>" disabled="" >
                    </div>

                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Password</label>
                    </div>
                    <div class="col">
                        <input type="password" class="form-control" name="data_password" disabled value="<%= session.getAttribute("password")%>">
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Numéro carte:</label>
                    </div>
                    <div class="col">
                        <input type="text" class="form-control" name="data_numero" value="<%= session.getAttribute("username")%>" disabled="" > 
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Montant en Banque:</label>
                    </div>
                    <div class="col">
                        <input type="text" class="form-control" name="data_montant" value="<%= session.getAttribute("montant")%>$" disabled="" >
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Niveau:</label>
                    </div>
                    <div class="col">
                        <input type="text" class="form-control" name="data_niveau" value="<%= session.getAttribute("profile")%>" disabled="" >
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Adresse:</label>
                    </div>
                    <div class="col">
                        <input type="text" class="form-control" name="data_adresse" value="<%= session.getAttribute("adresse")%>" disabled="" >
                    </div>
                </div>
            </form>
            <a class="btn btn-primary" href="UpdateInfos.jsp">Update infos</a>
        </div>
    </div>
    <%
        if (session.getAttribute("username") == null)
            response.sendRedirect(request.getContextPath() + "/Views/login.jsp");
    %>
</div>
<%@ include file="footer.jsp"%>
