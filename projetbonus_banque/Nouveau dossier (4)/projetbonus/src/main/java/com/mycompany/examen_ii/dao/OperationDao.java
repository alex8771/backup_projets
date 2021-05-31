/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.mycompany.examen_ii.dao;

import com.mycompany.examen_ii.utils.Utils;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Alexandre
 */
public class OperationDao {

    private final String SQL_DepotArgent = "INSERT INTO Operation (User_envoi,User_recep,Montant,Statut,Resultat,Montant_retrait,Motant_depot,idUser)values(?,?,?,?,?,?,?,?)";

    private Connection connect;
    private final Utils db_connect;
    private ResultSet rs;
    private PreparedStatement ps;

    public OperationDao() {
        db_connect = new Utils();
        connect = null;
        rs = null;
        ps = null;
    }

    public String CreateOperation(int userEnvois,int userRecois,double montant,String Statut,String Resultat,double retrait,double depot, String IdVendeur) throws SQLException {
        try {
            connect = db_connect.getConnect();
            ps = connect.prepareStatement(SQL_DepotArgent);
            ps.setDouble(1, userEnvois);
            ps.setInt(2, userRecois);
            ps.setDouble(3, montant);
            ps.setString(4, Statut);
            ps.setString(5, Resultat);
            ps.setDouble(6, retrait);
            ps.setDouble(7, depot);
            ps.setString(8, IdVendeur);
            ps.executeUpdate();

        } catch (SQLException ex) {
            Logger.getLogger(UserDAO.class.getName()).log(Level.SEVERE, null, ex);
        } finally {
            try {
                ps.close();
                db_connect.closeConnect();
            } catch (SQLException ex) {
                Logger.getLogger(UserDAO.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
        return "";
    }
}
