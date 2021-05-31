<%-- 
    Document   : operationsClient
    Created on : 2019-12-23, 15:19:43
    Author     : Alexandre
--%>

<%@ include file="header.jsp"%>
<%@ include file="navbar.jsp"%>
<div id="myGlobalContainer" class="d-flex align-items-center justify-content-center">
    <div class="card" id="operationCOmpte">
        <h5 class="card-header">Opérations</h5>
        <div class="card-body">
            <div class="form-group row">
                <div class="col">
                    <label>Montant en banque:</label>
                </div>
                <div class="col">
                    <input  type="text" class="form-control" disabled="" id="montantBanque" name="data_montantBanque" value="<%= session.getAttribute("montant")%>$">
                </div>
                <div class="d-flex justify-content-around">
                    <a class="btn btn-success" href="mettreArgent.jsp" >Mettre de l'argent</a>
                    <a class="btn btn-danger" href="retireArgent.jsp">Retirer de l'argent</a>
                    <a class="btn btn-primary" href="envoyerArgent.jsp">Envoyer de l'argent</a>
                </div>
            </div>
        </div>
        <%            if (session.getAttribute("fullname") == null)
                response.sendRedirect(request.getContextPath() + "/Views/login.jsp");
        %>
    </div>
    <%@ include file="footer.jsp"%>
