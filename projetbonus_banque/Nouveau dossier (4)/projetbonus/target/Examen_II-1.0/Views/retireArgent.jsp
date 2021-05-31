<%-- 
    Document   : retireArgent
    Created on : 2019-12-23, 15:27:07
    Author     : Alexandre
--%>

<%@ include file="header.jsp"%>
<%@ include file="navbar.jsp"%>
<div id="myGlobalContainer" class="d-flex align-items-center justify-content-center">
    <div class="card" id="retireCompte">
        <h5 class="card-header">Retire de l'argent</h5>
        <div class="card-body">
            <form action="${pageContext.request.contextPath}/EnleverArgent" method="post">
                <div class="form-group row">
                    <div class="col">
                        <label>Montant en banque:</label>
                    </div>
                    <div class="col">
                        <input  type="text" class="form-control" id="montantBanqueretirer" name="data_montantBanque_retire" value="<%= session.getAttribute("montant")%>">
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Montant:</label>
                    </div>
                    <div class="col">
                        <input  type="text" class="form-control" id="montantBanqueRetirer" name="montantBanque_retirer">
                    </div>
                </div>
                <div>
                    <button class="btn btn-success" id="retirerargentbouton" type="submit">Retirer de l'argent</button>
                </div>
            </form>
        </div>
    </div>
    <%            if (session.getAttribute("fullname") == null)
            response.sendRedirect(request.getContextPath() + "/Views/login.jsp");
    %>

    <% if (request.getAttribute("retirerArgent") == "false") { %>
    <div class="alert alert-success alert-dismissible fade show" role="alert">
        <b>Info :</b> Changer!
        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
    </div>
    <% } else if (request.getAttribute("retirerArgent") == "true") { %>
    <div class="alert alert-danger alert-dismissible fade show" role="alert">
        <b>Erreur :</b> Impossible !
        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
    </div>
    <% }%>
</div>
<%@ include file="footer.jsp"%>