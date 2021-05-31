<%-- 
    Document   : envoyerArgent
    Created on : 2019-12-23, 15:27:22
    Author     : Alexandre
--%>

<%@ include file="header.jsp"%>
<%@ include file="navbar.jsp"%>
<div id="myGlobalContainer" class="d-flex align-items-center justify-content-center">
    <div class="card" id="envoyerCompte">
        <h5 class="card-header">Envoyer de l'argent</h5>
        <div class="card-body">
            <form action="${pageContext.request.contextPath}/EnvoyerArgent" method="post">
                <div class="form-group row">
                    <div class="col">
                        <label>Montant en banque:</label>
                    </div>
                    <div class="col">
                        <input  type="text" class="form-control" disabled="" id="montantBanque" name="data_montantBanque_" value="<%= session.getAttribute("montant")%>$">
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Montant:</label>
                    </div>
                    <div class="col">
                        <input  type="text" class="form-control" id="montantBanqueEnvoyer" name="data_montantBanque_envoyer">
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Nom:</label>
                    </div>
                    <div class="col">
                        <input  type="text" class="form-control" id="PersonneBanqueEnvoyer" name="data_montantBanque_personne_envoyer">
                    </div>
                </div>
                <div>
                    <button class="btn btn-success" id="envoyerargentbouton" type="submit">Envoyer de l'argent</button>
                </div>
            </form>
        </div>
        <%            if (session.getAttribute("fullname") == null)
                response.sendRedirect(request.getContextPath() + "/Views/login.jsp");
        %>
    </div>
</div>
<%@ include file="footer.jsp"%>
