<%-- 
    Document   : mettreArgent
    Created on : 2019-12-23, 15:23:43
    Author     : Alexandre
--%>

<%@ include file="header.jsp"%>
<%@ include file="navbar.jsp"%>
<div id="myGlobalContainer" class="d-flex align-items-center justify-content-center">
    <div class="card" id="mettreCompte">
        <h5 class="card-header">Mettre de l'argent</h5>
        <div class="card-body">
            <form action="${pageContext.request.contextPath}/AjouterArgent" method="post">
                <div class="form-group row">
                    <div class="col">
                        <label>Montant en banque:</label>
                    </div>
                    <div class="col">
                        <input  type="text" class="form-control" id="montantBanqueMettre" name="data_montantBanque" value="<%= session.getAttribute("montant")%>">
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Montant:</label>
                    </div>
                    <div class="col">
                        <input  type="text" class="form-control" id="montantBanqueMettre" name="data_montantBanque_mettre">
                    </div>
                </div>
                <div>
                    <button class="btn btn-success" id="mettreargentbouton" type="submit">Mettre de l'argent</button>
                </div>
            </form>
        </div>


        <%            if (session.getAttribute("fullname") == null)
                response.sendRedirect(request.getContextPath() + "/Views/login.jsp");
        %>


        <% if (request.getAttribute("mettreArgent") == "false") { %>
        <div class="alert alert-success alert-dismissible fade show" role="alert">
            <b>Info :</b> Changer!
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <% } else if (request.getAttribute("mettreArgent") == "true") { %>
        <div class="alert alert-danger alert-dismissible fade show" role="alert">
            <b>Erreur :</b> Impossible !
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <% }%>
    </div>
</div>
<%@ include file="footer.jsp"%>
