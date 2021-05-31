/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.mycompany.examen_ii.dto;

import java.util.ArrayList;

/**
 *
 * @author yacin
 */
public class UserDTO {

    private int _id;
    private String _nom;
    private String _prenom;
    private String _email;
    private String _password;
    private String _numero;
    private double _montant;
    private String _niveau;
    private String _adresse;
    private double _prime;
    private double _transfert;
    private double _retrait;
    private int _idVendeur;

    public static ArrayList<UserDTO> userAffichetotal = new ArrayList<UserDTO>();
    public static ArrayList<UserDTO> userAffiche = new ArrayList<UserDTO>();
    public static ArrayList<UserDTO> vendeurAffiche = new ArrayList<UserDTO>();
    public static ArrayList<UserDTO> userEnvoyers = new ArrayList<UserDTO>();

    public UserDTO() {

    }

    public UserDTO(int id, String Nom, String Prenom, String Email, String Password, String NumeroCarte, double Montant, String Niveau, String Adresse, double Prime, double Transfert, double Retrait, int idVendeur) {
        this._id = id;
        this._nom = Nom;
        this._prenom = Prenom;
        this._email = Email;
        this._password = Password;
        this._numero = NumeroCarte;
        this._montant = Montant;
        this._niveau = Niveau;
        this._adresse = Adresse;
        this._prime = Prime;
        this._transfert = Transfert;
        this._retrait = Retrait;
        this._idVendeur = idVendeur;
    }

    public int getId() {
        return this._id;
    }

    public void setId(int id) {
        this._id = id;
    }

    public String getNom() {
        return this._nom;
    }

    public void setNom(String Nom) {
        this._nom = Nom;
    }

    public String getPrenom() {
        return this._prenom;
    }

    public void setPrenom(String Prenom) {
        this._prenom = Prenom;
    }

    public String getEmail() {
        return this._email;
    }

    public void setEmail(String Email) {
        this._email = Email;
    }

    public String getPassword() {
        return this._password;
    }

    public void setPassword(String Pasword) {
        this._password = Pasword;
    }

    public String getNumero() {
        return this._numero;
    }

    public void setNumero(String NumeroCarte) {
        this._numero = NumeroCarte;
    }

    public double getMontant() {
        return this._montant;
    }

    public void setMontant(double Montant) {
        this._montant = Montant;
    }

    public String getNiveau() {
        return this._niveau;
    }

    public void setNiveau(String Niveau) {
        this._niveau = Niveau;
    }

    public String getAdresse() {
        return this._adresse;
    }

    public void setAdresse(String Adresse) {
        this._adresse = Adresse;
    }

    public double getPrime() {
        return this._prime;
    }

    public void setPrime(double Prime) {
        this._prime = Prime;
    }

    public double getTransfert() {
        return this._transfert;
    }

    public void setTransfert(double Transfert) {
        this._transfert = Transfert;
    }

    public double getRetrait() {
        return this._retrait;
    }

    public void setRetrait(double Retrait) {
        this._retrait = Retrait;
    }

    public double getidvendeur() {
        return this._idVendeur;
    }

    public void setidvendeur(int idVendeur) {
        this._idVendeur = idVendeur;
    }

}
