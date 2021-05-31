<%-- 
    Document   : ListeClient
    Created on : 2019-12-30, 14:26:59
    Author     : Alexandre
--%>

<%@page import="java.util.ArrayList"%>
<%@page import="java.util.List"%>
<%@page import="com.mycompany.examen_ii.dto.UserDTO"%>
<jsp:include page="header.jsp">
    <jsp:param name="title" value="Accueil"/>
</jsp:include>
<%@ include file="navbar.jsp"%>
<h5>Table des users</h5>
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
                    <% List<UserDTO> listeVendeur = (ArrayList<UserDTO>) request.getAttribute("banque");
                        for (UserDTO v : listeVendeur) {
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
                            <form action="${pageContext.request.contextPath}/RemoveVendeur" method="post">
                                <input type="hidden" name="data_vendeur" value="<%= v.getId()%>">
                                <button type="submit" class="btn btn-danger">Supprimer</button>
                            </form>    
                            <form action="${pageContext.request.contextPath}/ModifierUser" method="post">
                                <input type="hidden" name="data_vendeur_modification" value="<%= v.getId()%>">
                                <button type="submit" class="btn btn-danger">Modifier</button>
                            </form>  
                        </td>
                    </tr>
                    <%}%> 
                </tbody>
            </table>
        </div>
    </form>
</div>