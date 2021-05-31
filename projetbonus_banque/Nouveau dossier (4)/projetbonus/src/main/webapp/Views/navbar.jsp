<nav class="navbar navbar-light d-flex flex-row-reverse">
    <ul class="nav nav-pills nav-fill">
        <li class="nav-item">
            <a class="nav-link" type="button" href="profil.jsp">Profil</a>           
        </li>
        <%
            if (session.getAttribute("profile").equals("DG")) {
        %>
        <li>
            <a class="nav-link" type="button" href="ajouterVendeurs.jsp">Ajouter des vendeurs</a>
        </li>
        <%
            }
            if (session.getAttribute("profile").equals("DG") || session.getAttribute("profile").equals("vendeur")) {
        %>
        <li>
            <a class="nav-link" type="button" href="${pageContext.request.contextPath}/ajouterClient">Ajouter des clients</a>
        </li>
        <%
            }
        %>
        <%
            if (session.getAttribute("profile").equals("DG")||session.getAttribute("profile").equals("vendeur")||session.getAttribute("profile").equals("client")) {
        %>
        <li>
            <a class="nav-link" type="button" href="${pageContext.request.contextPath}/pagePrincipale">Page principale</a>
        </li>
        <%
            }
        %>
        <%
            if (session.getAttribute("profile").equals("DG")) {
        %>
        <li>
            <a class="nav-link" type="button" href="${pageContext.request.contextPath}/listeTotal">Les listes</a>
        </li>
        <%
            }
        %>
        <%
            if (session.getAttribute("profile").equals("vendeur")) {
        %>
        <li>
            <a class="nav-link" type="button" href="${pageContext.request.contextPath}/listeClient">Liste des clients</a>
        </li>
        <%
            }
        %>
        <%
            if (session.getAttribute("profile").equals("client")) {
        %>
        <li>
            <a class="nav-link" type="button" href="operationsClient.jsp">Opérations</a>
        </li>
        <%
            }
        %>
        <li class="nav-item">
            <a class="nav-link" href="${pageContext.request.contextPath}/Deconnecter">Se déconnecter</a>
        </li>
    </ul>
</nav>