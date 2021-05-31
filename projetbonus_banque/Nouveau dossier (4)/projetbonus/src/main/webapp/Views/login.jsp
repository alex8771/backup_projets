<%-- 
    Document   : login
    Created on : 5 nov. 2019, 18:41:12
    Author     : yacin
--%>

<jsp:include page="header.jsp">
    <jsp:param name="title" value="Se connecter"/>
</jsp:include>

<div id="myGlobalContainer" class="d-flex align-items-center justify-content-center">
    <div class="card" id="connect">
        <h5 class="card-header">Connexion</h5>
        <div class="card-body">

            <form method="POST" action="${pageContext.request.contextPath}/Connecter" >
                <div class="form-group row">
                    <div class="col">
                        <label>Identifiant :</label>
                    </div>
                    <div class="col">
                        <input type="text" class="form-control" name="data_login" placeholder="Entrez votre identifiant">
                    </div>
                </div>

                <div class="form-group row">
                    <div class="col">
                        <label>Mot de Passe :</label>
                    </div>
                    <div class="col">
                        <input type="password" class="form-control" name="data_password" placeholder="Entrez votre mot de passe">
                    </div>
                </div>

                <div>
                    <button class="btn btn-danger" type="reset">Réinitialiser</button>
                    <button class="btn btn-success float-right" type="submit">Se Connecter</button>
                </div>
            </form><br>

            <% if(request.getAttribute("erreur") == "true"){ %>
            <div class="alert alert-danger alert-dismissible fade show" role="alert">
                <b>Erreur :</b> L'identifiant ou le mot de passe !
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <% } %>

            <% 
                if(session.getAttribute("fullname") != null)
                    response.sendRedirect(request.getContextPath() + "/Views/home.jsp");
            %>
        </div>
    </div>
</div>