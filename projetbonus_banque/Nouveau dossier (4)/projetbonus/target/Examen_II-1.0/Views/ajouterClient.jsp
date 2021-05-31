<%-- 
    Document   : ajouterClient
    Created on : 2019-12-21, 13:11:22
    Author     : Alexandre
--%>

<%@page import="java.util.ArrayList"%>
<%@page import="java.util.List"%>
<%@page import="com.mycompany.examen_ii.dto.UserDTO"%>
<%@ include file="header.jsp"%>
<%@ include file="navbar.jsp"%>
<div id="myGlobalContainer" class="d-flex align-items-center justify-content-center">
    <div class="card" id="connect">
        <h5 class="card-header">Ajouter des clients</h5>
        <div class="card-body">
            <form action="${pageContext.request.contextPath}/AddClient" method="post">
                <div class="form-group row">
                    <div class="col">
                        <label>Liste des commerciants</label>
                    </div>
                    <div class="btn-group col">
                        <select class="col" name="liste">
                            <% List<UserDTO> listeVendeur = (ArrayList<UserDTO>) request.getAttribute("vendeurrelier");
                                for (UserDTO v : listeVendeur) {
                            %>                           
                            <option value=<%=v.getId()%>><%=v.getId()%></option>
                            <%}%> 
                        </select>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Nom:</label>
                    </div>
                    <div class="col">
                        <input  type="text" class="form-control" id="nom" name="new_data_nom">
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Prénom:</label>
                    </div>
                    <div class="col">
                        <input  type="text" class="form-control" id="prenom" name="new_data_prenom">
                    </div>
                </div> 
                <div class="form-group row">
                    <div class="col">
                        <label>Adresse email:</label>
                    </div>
                    <div class="col">
                        <input  type="text" class="form-control" name="new_data_email" >
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Mot de Passe:</label>
                    </div>
                    <div class="col">
                        <input type="password" class="form-control" name="new_data_password" >
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Confirmer mot de passe:</label>
                    </div>
                    <div class="col">
                        <input  type="password" class="form-control" name="data_password_confirme">
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Numero de carte:</label>
                    </div>
                    <div class="col">
                        <input type="text" class="form-control" id="identifiantIns" name="data_carte_numero">
                    </div>
                    <div>
                        <button type="button" class="btn btn-primary" id="Generateur">Aléatoire</button>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Montant en banque:</label>
                    </div>
                    <div class="col">
                        <input  type="text" class="form-control" name="data_montant_banque">
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Niveau:</label>
                    </div>
                    <div class="col">
                        <input  type="text" class="form-control" name="data_niveau" value="client">
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Adresse:</label>
                    </div>
                    <div class="col">
                        <input  type="text" class="form-control" name="data_adresse">
                    </div>
                </div>      
                <div class="form-group row">
                    <div class="col">
                        <label>Prime:</label>
                    </div>
                    <div class="col">
                        <input  type="text" class="form-control" name="data_prime" value="0">
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Frais transfert:</label>
                    </div>
                    <div class="col">
                        <input  type="text" class="form-control" name="data_tranfert" value="0.75">
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col">
                        <label>Retrait d'argent:</label>
                    </div>
                    <div class="col">
                        <input  type="text" class="form-control" name="data_retrait" value="1.20">
                    </div>
                </div>
                <div class="d-flex justify-content-around">
                    <a class="btn btn-danger" href="${pageContext.request.contextPath}/pagePrincipale">Retour</a>
                    <button class="btn btn-success" type="submit">Valider</button>
                </div>
            </form>
            <% if (request.getAttribute(
                        "erreur") == "confirme") { %>
            <div class="alert alert-danger" role="alert">
                <b>Erreur :</b> Les mots de passe ne sont pas identique!
            </div>
            <% }%>
            <% if (request.getAttribute(
                        "erreur") == "id" || request.getAttribute("erreur") == "email") {%>
            <div class="alert alert-danger" role="alert">
                <b>Erreur :</b> Votre <%= request.getAttribute("erreur")%> existe déjà dans la base de données.
            </div>
            <% }%>
        </div>
    </div>            
</div>
<%@ include file="footer.jsp"%>
