/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.mycompany.examen_ii.utils;

import static java.lang.System.out;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author yacin
 */
public class Utils {

    private Connection connect;
    private String url;
    private String user;
    private String pass;

    public Connection getConnect() {

        url = "jdbc:mariadb://1898182.ddns.net:3306/banque_Alex_Tristan?useUnicode=true&useSSL=false&useJDBCCompliantTimezoneShift=true&useLegacyDatetimeCode=false&serverTimezone=UTC";
        user = "Alex";
        pass = "#abc123";

        try {
            Class.forName("org.mariadb.jdbc.Driver");
            try {
                connect = DriverManager.getConnection(url, user, pass);
                //out.println("Connexion réussie !");
            } catch (SQLException ex) {
                Logger.getLogger(Utils.class.getName()).log(Level.SEVERE, null, ex);
                //out.println("Connexion échouée !");
            }

        } catch (ClassNotFoundException ex) {
            Logger.getLogger(Utils.class.getName()).log(Level.SEVERE, null, ex);
            // out.println("Connexion échouée2 !");
        }

        return connect;
    }

    public void closeConnect() {
        try {
            if (!connect.isClosed()) {
                connect.close();
            }
        } catch (SQLException ex) {
            Logger.getLogger(Utils.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
}
