<%-- 
    Document   : liste
    Created on : 2019-12-21, 13:14:42
    Author     : Alexandre
--%>

<%@page import="java.util.ArrayList"%>
<%@page import="java.util.List"%>
<%@page import="com.mycompany.examen_ii.dto.UserDTO"%>
<jsp:include page="header.jsp">
    <jsp:param name="title" value="Accueil"/>
</jsp:include>
<%@ include file="navbar.jsp"%>
<h5>Table des users et vendeurs</h5>
<div class="card-body" id="scoreAffichage">
    <form>
        <div class="card-body scrollList">
            <div>
                <label></span>Vendeurs</label>
            </div>
            <table class="table" id="tableVendeurs">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Nom</th>
                        <th scope="col">Prénom</th>
                        <th scope="col">Email</th>
                        <th scope="col">Password</th>
                        <th scope="col">Numéro de carte</th>
                        <th scope="col">Montant en banque</th>
                        <th scope="col">Niveau</th>
                        <th scope="col">Adresse</th>
                        <th scope="col">Prime</th>
                        <th scope="col">Frais de Transfert</th>
                        <th scope="col">Frais de Retrait</th>
                        <th scope="col">Actions</th>
                    </tr>
                </thead>                            
                <tbody id="listeVendeurs" name="listeVendeurs"> 
                    <% List<UserDTO> listeVendeur = (ArrayList<UserDTO>) request.getAttribute("data");
                        for (UserDTO v : listeVendeur) {
                            if (v.getNiveau().equals("vendeur")) {
                    %>
                    <tr>
                        <td><%=v.getId()%></td>
                        <td><%=v.getNom()%></td>
                        <td><%=v.getPrenom()%></td>
                        <td><%=v.getEmail()%></td>
                        <td><%=v.getPassword()%></td>
                        <td><%=v.getNumero()%></td>
                        <td><%=v.getMontant()%></td>
                        <td><%=v.getNiveau()%></td>
                        <td><%=v.getAdresse()%></td>
                        <td><%=v.getPrime()%></td>
                        <td><%=v.getTransfert()%></td>
                        <td><%=v.getRetrait()%></td>
                        <td class="form-group row">
                            <div>
                                <form action="${pageContext.request.contextPath}/RemoveVendeur" method="post">
                                    <input type="hidden" name="data_vendeur" value="<%= v.getId()%>">
                                    <button type="submit" class="btn btn-danger">Supprimer</button>
                                </form> 
                            </div>
                            <div>
                                <form action="${pageContext.request.contextPath}/ModifierUser" method="post">
                                    <input type="hidden" name="data_vendeur_modification" value="<%= v.getId()%>">
                                    <button type="submit" class="btn btn-danger">Modifier</button>
                                </form>  
                            </div>
                        </td>
                    </tr>
                    <%}
                        }%> 
                </tbody>
            </table>
        </div>
        <div class="card-body scrollList">
            <div>
                <label></span>Client</label>
            </div>
            <table class="table" id="tableClient">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Nom</th>
                        <th scope="col">Prénom</th>
                        <th scope="col">Email</th>
                        <th scope="col">Password</th>
                        <th scope="col">Numéro de carte</th>
                        <th scope="col">Montant en banque</th>
                        <th scope="col">Niveau</th>
                        <th scope="col">Adresse</th>
                        <th scope="col">Prime</th>
                        <th scope="col">Frais de Transfert</th>
                        <th scope="col">Frais de Retrait</th>
                        <th scope="col">Actions</th>
                    </tr>
                </thead>
                <tbody id="listeCLient" name="listeCLient"> 
                    <%
                        List<UserDTO> listeClient = (ArrayList<UserDTO>) request.getAttribute("data");
                        for (UserDTO c : listeClient) {
                            if (c.getNiveau().equals("client")) {
                    %>
                    <tr>
                        <td><%=c.getId()%></td>
                        <td><%=c.getNom()%></td>
                        <td><%=c.getPrenom()%></td>
                        <td><%=c.getEmail()%></td>
                        <td><%=c.getPassword()%></td>
                        <td><%=c.getNumero()%></td>
                        <td><%=c.getMontant()%></td>
                        <td><%=c.getNiveau()%></td>
                        <td><%=c.getAdresse()%></td>
                        <td><%=c.getPrime()%></td>
                        <td><%=c.getTransfert()%></td>
                        <td><%=c.getRetrait()%></td>
                        <td class="form-group row">
                            <div>
                                <form action="${pageContext.request.contextPath}/RemoveUser" method="post">
                                    <input type="hidden" name="data_user" value="<%= c.getId()%>">
                                    <button type="submit" class="btn btn-danger">Supprimer</button>
                                </form> 
                            </div>
                            <div>
                                <form action="${pageContext.request.contextPath}/ModifierUser" method="post">
                                    <input type="hidden" name="data_user_modification" value="<%= c.getId()%>">
                                    <button type="submit" class="btn btn-danger">Modifier</button>
                                </form> 
                            </div>
                        </td>
                    </tr>
                    <%}
                        }%> 
                </tbody>
            </table>
        </div>
    </form>
</div>
<%@ include file="footer.jsp"%>