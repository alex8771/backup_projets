/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.mycompany.examen_ii.dao;

import com.mycompany.examen_ii.dto.UserDTO;
import com.mycompany.examen_ii.utils.Utils;
import static java.lang.System.out;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.LinkedList;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author yacin
 */
public class UserDAO {

    private final String SQL_GetUser = "SELECT * FROM User WHERE NumeroCarte=? AND Password=?";

    private final String SQL_UpdatePassword = "UPDATE User SET Password=? WHERE NumeroCarte=?";
    private final String SQL_UpdateAdresse = "UPDATE User SET Adresse=? WHERE NumeroCarte=?";
    private final String SQL_UpdateEmail = "UPDATE User SET Email=? WHERE NumeroCarte=?";

    private final String SQL_CreateUser = "INSERT INTO User(Nom, Prenom, Email, Password,Numerocarte,MontantBanque,Niveau,Adresse,Prime,FraisTransfert,FraisRetrait,idVendeur) VALUES(?, ?, ?, ?,?,?,?,?,?,?,?,?)";
    private final String SQL_GetListe = "SELECT * FROM User";
    private final String SQL_GetListeID = "SELECT * FROM User where idVendeur=?";

    private final String SQL_RemoveUser = "DELETE FROM User WHERE idUser=?";

    private final String SQL_Montant = "UPDATE User SET MontantBanque=? WHERE NumeroCarte=?";

    private final String SQL_Tranfert = "UPDATE User SET FraisTransfert=? WHERE Niveau=?";
    private final String SQL_Retrait = "UPDATE User SET FraisRetrait=? WHERE Niveau=?";

    private final String SQL_ListeVendeur = "Select idUser FROM User where Niveau=?";
    private final String SQL_PersonneEnvoyer = "Select NumeroCarte from User WHERE NumeroCarte=?";

    //private final String SQL_ModificationUser = "Select * from User WHERE idUser=?";
    //private final String SQL_Verifier = "SELECT Email, NumeroCarte FROM User";
    public static List<UserDTO> user = new LinkedList<UserDTO>();
    public static List<UserDTO> vendeur = new LinkedList<UserDTO>();
    public static List<UserDTO> userEnvoyer = new LinkedList<UserDTO>();

    private Connection connect;
    private final Utils db_connect;
    private ResultSet rs;
    private PreparedStatement ps;

    public UserDAO() {
        db_connect = new Utils();
        connect = null;
        rs = null;
        ps = null;
    }

    public List<UserDTO> GetListe() throws SQLException {
        try {
            user.clear();
            connect = db_connect.getConnect();
            ps = connect.prepareStatement(SQL_GetListe);
            rs = ps.executeQuery();

            if (rs != null) {
                while (rs.next()) {
                    UserDTO users = new UserDTO();
                    users.setId(rs.getInt(1));
                    users.setNom(rs.getString(2));
                    users.setPrenom(rs.getString(3));
                    users.setEmail(rs.getString(4));
                    users.setPassword(rs.getString(5));
                    users.setNumero(rs.getString(6));
                    users.setMontant(rs.getDouble(7));
                    users.setNiveau(rs.getString(8));
                    users.setAdresse(rs.getString(9));
                    users.setPrime(rs.getDouble(10));
                    user.add(users);
                }
            }
        } catch (SQLException ex) {
            Logger.getLogger(UserDAO.class.getName()).log(Level.SEVERE, null, ex);
        } finally {
            try {
                rs.close();
                db_connect.closeConnect();
            } catch (SQLException ex) {
                Logger.getLogger(UserDAO.class.getName()).log(Level.SEVERE, null, ex);
            }
        }

        return user;
    }

    public List<UserDTO> GetListeSpecifique(int valeur) throws SQLException {
        try {
            user.clear();
            connect = db_connect.getConnect();
            out.println(valeur);
            ps = connect.prepareStatement(SQL_GetListeID);
            ps.setInt(1, valeur);
            rs = ps.executeQuery();

            if (rs != null) {
                while (rs.next()) {
                    UserDTO users = new UserDTO();
                    users.setId(rs.getInt(1));
                    users.setNom(rs.getString(2));
                    users.setPrenom(rs.getString(3));
                    users.setEmail(rs.getString(4));
                    users.setPassword(rs.getString(5));
                    users.setNumero(rs.getString(6));
                    users.setMontant(rs.getDouble(7));
                    users.setNiveau(rs.getString(8));
                    users.setAdresse(rs.getString(9));
                    users.setPrime(rs.getDouble(10));
                    user.add(users);
                }
            }
        } catch (SQLException ex) {
            Logger.getLogger(UserDAO.class.getName()).log(Level.SEVERE, null, ex);
        } finally {
            try {
                rs.close();
                db_connect.closeConnect();
            } catch (SQLException ex) {
                Logger.getLogger(UserDAO.class.getName()).log(Level.SEVERE, null, ex);
            }
        }

        return user;
    }

    public UserDTO GetUser(String username, String password) throws SQLException {
        try {

            connect = db_connect.getConnect();
            ps = connect.prepareStatement(SQL_GetUser);
            ps.setString(1, username);
            ps.setString(2, password);
            rs = ps.executeQuery();

            UserDTO userDTO = new UserDTO();

            if (rs != null) {

                while (rs.next()) {

                    userDTO.setId(rs.getInt(1));
                    userDTO.setNom(rs.getString(2));
                    userDTO.setPrenom(rs.getString(3));
                    userDTO.setEmail(rs.getString(4));
                    userDTO.setPassword(rs.getString(5));
                    userDTO.setNumero(rs.getString(6));
                    userDTO.setMontant(rs.getDouble(7));
                    userDTO.setNiveau(rs.getString(8));
                    userDTO.setAdresse(rs.getString(9));
                    userDTO.setPrime(rs.getDouble(10));
                    userDTO.setTransfert(rs.getDouble(11));
                    userDTO.setRetrait(rs.getDouble(12));
                    userDTO.setidvendeur(rs.getInt(13));
                    return userDTO;
                }
            }

        } catch (SQLException ex) {
            Logger.getLogger(UserDAO.class.getName()).log(Level.SEVERE, null, ex);
        } finally {
            try {
                rs.close();
                db_connect.closeConnect();
            } catch (SQLException ex) {
                Logger.getLogger(UserDAO.class.getName()).log(Level.SEVERE, null, ex);
            }
        }

        return null;
    }

    public void UpdatePassword(String username, String new_password) throws SQLException {
        try {
            connect = db_connect.getConnect();
            ps = connect.prepareStatement(SQL_UpdatePassword);
            ps.setString(1, new_password);
            ps.setString(2, username);
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
    }

    public void UpdateAdresse(String username, String new_adresse) throws SQLException {
        try {
            connect = db_connect.getConnect();
            ps = connect.prepareStatement(SQL_UpdateAdresse);
            ps.setString(1, new_adresse);
            ps.setString(2, username);
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
    }

    public void UpdateEmail(String username, String new_email) throws SQLException {
        try {
            connect = db_connect.getConnect();
            ps = connect.prepareStatement(SQL_UpdateEmail);
            ps.setString(1, new_email);
            ps.setString(2, username);
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
    }

    public String CreateUser(String name, String prenom, String Email, String Password, String NumeroCarte, double Montant, String Niveau, String Adresse, double Prime, double Transfert, double Retrait, double IdVendeur) throws SQLException {
        try {
            connect = db_connect.getConnect();
            ps = connect.prepareStatement(SQL_CreateUser);
            ps.setString(1, name);
            ps.setString(2, prenom);
            ps.setString(3, Email);
            ps.setString(4, Password);
            ps.setString(5, NumeroCarte);
            ps.setDouble(6, Montant);
            ps.setString(7, Niveau);
            ps.setString(8, Adresse);
            ps.setDouble(9, Prime);
            ps.setDouble(10, Transfert);
            ps.setDouble(11, Retrait);
            ps.setDouble(12, IdVendeur);
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

    public void RemoveUser(int id) throws SQLException {
        try {
            connect = db_connect.getConnect();
            ps = connect.prepareStatement(SQL_RemoveUser);
            
            ps.setInt(1, id);
            ps.executeUpdate();
            System.out.print(ps.toString());

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
    }

    public void UpdateMontant(String username, Double montant) throws SQLException {
        try {
            connect = db_connect.getConnect();
            ps = connect.prepareStatement(SQL_Montant);
            ps.setDouble(1, montant);
            ps.setString(2, username);
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
    }

    public void UpdateTranfert(Double montant, String niveau) throws SQLException {
        try {
            connect = db_connect.getConnect();

            ps = connect.prepareStatement(SQL_Tranfert);
            ps.setDouble(1, montant);
            ps.setString(2, niveau);
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
    }

    public void UpdateRetrait(Double montant, String niveau) throws SQLException {
        try {
            connect = db_connect.getConnect();

            ps = connect.prepareStatement(SQL_Retrait);
            ps.setDouble(1, montant);
            ps.setString(2, niveau);
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
    }

    public List<UserDTO> GetListeVendeur(String niveau) throws SQLException {
        try {
            vendeur.clear();
            connect = db_connect.getConnect();
            ps = connect.prepareStatement(SQL_ListeVendeur);
            ps.setString(1, niveau);
            rs = ps.executeQuery();

            if (rs != null) {
                while (rs.next()) {
                    UserDTO users = new UserDTO();
                    users.setId(rs.getInt(1));
                    user.add(users);
                }
            }
        } catch (SQLException ex) {
            Logger.getLogger(UserDAO.class.getName()).log(Level.SEVERE, null, ex);
        } finally {
            try {
                rs.close();
                db_connect.closeConnect();
            } catch (SQLException ex) {
                Logger.getLogger(UserDAO.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
        return user;
    }

    public List<UserDTO> PersonneEnvoyer(String nom) {
        try {
            userEnvoyer.clear();
            connect = db_connect.getConnect();
            ps = connect.prepareStatement(SQL_PersonneEnvoyer);
            ps.setString(1, nom);
            rs = ps.executeQuery();

            if (rs != null) {
                while (rs.next()) {
                    UserDTO users = new UserDTO();
                    users.setNumero(rs.getString(1));
                    userEnvoyer.add(users);
                }
            }
        } catch (SQLException ex) {
            Logger.getLogger(UserDAO.class.getName()).log(Level.SEVERE, null, ex);
        } finally {
            try {
                rs.close();
                db_connect.closeConnect();
            } catch (SQLException ex) {
                Logger.getLogger(UserDAO.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
        return userEnvoyer;
    }

    /* public String Verifier_email_numeroCarte_dresse(String email, int numerocarte, String adresse) {
        try {
            connect = db_connect.getConnect();
            ps = connect.prepareStatement(SQL_Verifier);
            rs = ps.executeQuery();

            if (rs != null) {
                while (rs.next()) {

                    if (rs.getString(1).equals(email)) {
                        return "email";
                    } else {
                        return "numerocarte";
                    }
                }
            }
        } catch (SQLException ex) {
            Logger.getLogger(UserDAO.class.getName()).log(Level.SEVERE, null, ex);
        } finally {
            try {
                rs.close();
                db_connect.closeConnect();
            } catch (SQLException ex) {
                Logger.getLogger(UserDAO.class.getName()).log(Level.SEVERE, null, ex);
            }
        }

        return "";
    }*/
 /*
    public UserDTO ModificationUser(int Id) throws SQLException {
        try {

            connect = db_connect.getConnect();
            ps = connect.prepareStatement(SQL_ModificationUser);
            ps.setInt(1, Id);
            rs = ps.executeQuery();

            UserDTO userDTO = new UserDTO();

            if (rs != null) {

                while (rs.next()) {

                    userDTO.setId(rs.getInt(1));
                    userDTO.setNom(rs.getString(2));
                    userDTO.setPrenom(rs.getString(3));
                    userDTO.setEmail(rs.getString(4));
                    userDTO.setPassword(rs.getString(5));
                    userDTO.setNumero(rs.getString(6));
                    userDTO.setMontant(rs.getDouble(7));
                    userDTO.setNiveau(rs.getString(8));
                    userDTO.setAdresse(rs.getString(9));
                    userDTO.setPrime(rs.getDouble(10));
                    userDTO.setTransfert(rs.getDouble(11));
                    userDTO.setRetrait(rs.getDouble(12));
                    return userDTO;
                }
            }

        } catch (SQLException ex) {
            Logger.getLogger(UserDAO.class.getName()).log(Level.SEVERE, null, ex);
        } finally {
            try {
                rs.close();
                db_connect.closeConnect();
            } catch (SQLException ex) {
                Logger.getLogger(UserDAO.class.getName()).log(Level.SEVERE, null, ex);
            }
        }

        return null;
    }*/
}
