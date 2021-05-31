/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.mycompany.examen_ii.servlets;

import com.mycompany.examen_ii.dao.UserDAO;
import java.io.IOException;
import java.io.PrintWriter;
import static java.lang.System.out;
import java.sql.SQLException;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

/**
 *
 * @author Alexandre
 */
@WebServlet(name = "EnleverArgent", urlPatterns = {"/EnleverArgent"})
public class EnleverArgent extends HttpServlet {

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
            out.println("<title>Servlet EnleverArgent</title>");
            out.println("</head>");
            out.println("<body>");
            out.println("<h1>Servlet EnleverArgent at " + request.getContextPath() + "</h1>");
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

        String montantBanque = request.getParameter("data_montantBanque_retire");
        Double montantcompte = Double.parseDouble(montantBanque);
        String montant = request.getParameter("montantBanque_retirer");
        double montantpoche = Double.parseDouble(montant);

        UserDAO userDao = new UserDAO();

        HttpSession session = request.getSession();
        String numero = session.getAttribute("username").toString();
        String interet = session.getAttribute("retrait").toString();

        double interetRetrait = Double.parseDouble(interet);
        montantpoche = montantpoche + (montantpoche * interetRetrait);
        montantcompte = montantcompte - montantpoche;

        try {
            userDao.UpdateMontant(numero, montantcompte);
            request.setAttribute("retirerArgent", "false");
        } catch (SQLException ex) {
            Logger.getLogger(UpdateInfo.class.getName()).log(Level.SEVERE, null, ex);
            request.setAttribute("retirerArgent", "true");
        }
        response.sendRedirect(request.getContextPath() + "/Views/profil.jsp");
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
