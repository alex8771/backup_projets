/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.mycompany.examen_ii.servlets;

import com.mycompany.examen_ii.dao.UserDAO;
import java.io.IOException;
import java.io.PrintWriter;
import java.sql.SQLException;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/**
 *
 * @author Alexandre
 */
@WebServlet(name = "AddVendeur", urlPatterns = {"/AddVendeur"})
public class AddVendeur extends HttpServlet {

    /**
     * Processes requests for both HTTP <code>GET</code> and <code>POST</code>
     * methods.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    protected void processRequest(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        response.setContentType("text/html;charset=UTF-8");
        try (PrintWriter out = response.getWriter()) {
            /* TODO output your page here. You may use following sample code. */
            out.println("<!DOCTYPE html>");
            out.println("<html>");
            out.println("<head>");
            out.println("<title>Servlet AddVendeur</title>");
            out.println("</head>");
            out.println("<body>");
            out.println("<h1>Servlet AddVendeur at " + request.getContextPath() + "</h1>");
            out.println("</body>");
            out.println("</html>");
        }
    }

    // <editor-fold defaultstate="collapsed" desc="HttpServlet methods. Click on the + sign on the left to edit the code.">
    /**
     * Handles the HTTP <code>GET</code> method.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        processRequest(request, response);
    }

    /**
     * Handles the HTTP <code>POST</code> method.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        //processRequest(request, response);
        String name = request.getParameter("new_data_nom");
        double idvendeur=0;
        String prenom = request.getParameter("new_data_prenom");
        String email = request.getParameter("new_data_email");
        String password = request.getParameter("new_data_password");
        String passwordConfirme = request.getParameter("data_password_confirme");
        String carte = request.getParameter("data_carte_numero");
        String montant = request.getParameter("data_montant_banque");
        double montantcompte = Double.parseDouble(montant);
        String niveau = request.getParameter("data_niveau");
        String adresseDomicile = request.getParameter("data_adresse");

        String prime = request.getParameter("data_prime");
        double primeBanque = Double.parseDouble(prime);
        String transfert = request.getParameter("data_tranfert");
        double fraisTransfert = Double.parseDouble(transfert);
        String retrait = request.getParameter("data_retrait");
        double fraisRetrait = Double.parseDouble(retrait);

        if (passwordConfirme.equals(password)) {
            UserDAO userDao = new UserDAO();

            try {
                userDao.CreateUser(name, prenom, email, password, carte, montantcompte, niveau, adresseDomicile, primeBanque, fraisTransfert, fraisRetrait,idvendeur);
                this.getServletContext().getRequestDispatcher("/Views/profil.jsp").forward(request, response);

            } catch (SQLException ex) {
                Logger.getLogger(Login.class.getName()).log(Level.SEVERE, null, ex);
            }
        } else {
            request.setAttribute("erreur", "confirme");
            this.getServletContext().getRequestDispatcher("/Views/ajouterClient.jsp").forward(request, response);
        }
    }

    /**
     * Returns a short description of the servlet.
     *
     * @return a String containing servlet description
     */
    @Override
    public String getServletInfo() {
        return "Short description";
    }// </editor-fold>

}
