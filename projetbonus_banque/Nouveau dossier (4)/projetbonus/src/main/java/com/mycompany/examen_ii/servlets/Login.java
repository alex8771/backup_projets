/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.mycompany.examen_ii.servlets;

import com.mycompany.examen_ii.dao.UserDAO;
import com.mycompany.examen_ii.dto.UserDTO;
import com.mycompany.examen_ii.utils.Utils;
import java.io.IOException;
import java.io.PrintWriter;
import static java.lang.System.out;
import java.sql.Connection;
import java.sql.SQLException;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

/**
 *
 * @author yacin
 */
public class Login extends HttpServlet {

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
            out.println("<title>Servlet Login</title>");
            out.println("</head>");
            out.println("<body>");
            out.println("<h1>Servlet Login at " + request.getContextPath() + "</h1>");
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
        // processRequest(request, response);
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

        String username = request.getParameter("data_login");
        String password = request.getParameter("data_password");

        UserDTO userDTO = null;
        UserDAO userDAO = new UserDAO();
        try {
            userDTO = userDAO.GetUser(username, password);

        } catch (SQLException ex) {
            Logger.getLogger(Login.class.getName()).log(Level.SEVERE, null, ex);
        }

        if (userDTO != null) {
            HttpSession session = request.getSession();
            session.setAttribute("id", userDTO.getId());
            session.setAttribute("fullname", userDTO.getPrenom());
            session.setAttribute("nom", userDTO.getNom());
            session.setAttribute("email", userDTO.getEmail());
            session.setAttribute("username", userDTO.getNumero());
            session.setAttribute("profile", userDTO.getNiveau());
            session.setAttribute("password", userDTO.getPassword());
            session.setAttribute("adresse", userDTO.getAdresse());
            session.setAttribute("montant", userDTO.getMontant());
            session.setAttribute("prime", userDTO.getPrime());
            session.setAttribute("transferts", userDTO.getTransfert());
            session.setAttribute("retrait", userDTO.getRetrait());
            session.setAttribute("idvendeur",userDTO.getidvendeur());

            switch (userDTO.getNiveau()) {
                case "client":
                    response.sendRedirect(request.getContextPath() + "/Views/client.jsp");
                    break;
                case "DG":
                    response.sendRedirect(request.getContextPath() + "/Views/dg.jsp");
                    break;
                default:
                    response.sendRedirect(request.getContextPath() + "/Views/vendeur.jsp");
                    break;
            }
        } else {
            request.setAttribute("erreur", "true");
            this.getServletContext().getRequestDispatcher("/Views/login.jsp").forward(request, response);
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
