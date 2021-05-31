/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.mycompany.examen_ii.dto;

/**
 *
 * @author Alexandre
 */
public class OperationDTO {

    private int _id;
    private int _userEnvois;
    private int _userRecois;
    private double _montant;
    private String _statut;
    private String _resultat;
    private double _montantRetrait;
    private double _montantDepot;

    public OperationDTO(int id, int userEnvois, int userRecois, double montant, String statut, String resultat, double montantRetrait, double montantDepot) {
        this._id = id;
        this._userEnvois = userEnvois;
        this._userRecois = userRecois;
        this._montant = montant;
        this._statut = statut;
        this._resultat = resultat;
        this._montantRetrait = montantRetrait;
        this._montantDepot = montantDepot;
    }

    public int getId() {
        return this._id;
    }

    public void setId(int id) {
        this._id = id;
    }

    public int getUserEnvois() {
        return this._userEnvois;
    }

    public void setUserEnvois(int userEnvois) {
        this._userEnvois = userEnvois;
    }

    public int getUserRecois() {
        return this._userRecois;
    }

    public void setUserRecois(int userRecois) {
        this._userRecois = userRecois;
    }

    public double getMontant() {
        return this._montant;
    }

    public void setMontant(double montant) {
        this._montant = montant;
    }

    public String getStatut() {
        return this._statut;
    }

    public void setStatut(String statut) {
        this._statut = statut;
    }

    public String getResultat() {
        return this._resultat;
    }

    public void setResultat(String resultat) {
        this._resultat = resultat;
    }

    public double getMontantRetrait() {
        return this._montantRetrait;
    }

    public void setMontantRetrait(double montantRetrait) {
        this._montantRetrait = montantRetrait;
    }

    public double getMontantDepot() {
        return this._montantDepot;
    }

    public void setMontantDepot(double montantDepot) {
        this._montantDepot = montantDepot;
    }
}
