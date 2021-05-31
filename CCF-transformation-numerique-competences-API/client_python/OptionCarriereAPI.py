import platform
import socket
import time
from datetime import datetime

import pkg_resources
import pymongo
from careerjet_api import CareerjetAPIClient

from client_python.job_offer_api import JobOfferAPI


class OptionCarriereAPI(JobOfferAPI):

    def __init__(self, config):
        """
        Initialized OptionCarriereAPI
        Initialize ip, call, connection, database, collection, api
        """
        JobOfferAPI.__init__(self, config)
        self.ip = OptionCarriereAPI.ipConfig(self)
        self.call = OptionCarriereAPI.setDictKeyLoc(self)
        self.connection = pymongo.MongoClient('mongodb://localhost:27017/')
        self.database = self.connection[self.config.api["database"]]
        self.collection = self.database[self.config.api["nom_api"]]
        self.api = CareerjetAPIClient(locale_code=self.config.api.locale_code)

    def callExternalAPI(self):
        """
        Call the external API
        :return: result of the external API class as dictionary
        """
        response = self.api.search(self.call)
        time.sleep(5)
        return response

    def ipConfig(self):
        """
        Get hostname pc
        :return: Ip Address
        """
        return socket.gethostbyname(socket.gethostname())

    def setDictKeyLoc(self):
        """
        Set a dictionnary with received data.
        """
        my_dict = {}
        list_keyword = ""
        list_location = ""
        if self.config.api.keywords is not None:
            list_keyword = " ".join(self.config.api.keywords)
        if self.config.api.location is not None:
            list_location = " ".join(self.config.api.location)
        my_dict["keywords"] = list_keyword
        my_dict["location"] = list_location
        my_dict["affid"] = self.config.api.affid
        my_dict["user_ip"] = self.ip
        my_dict["user_agent"] = self.config.api.user_agent
        my_dict["url"] = self.config.api.url
        my_dict["locale_code"] = self.config.api.locale_code
        return my_dict

    def addMetadata(self, job_offers):
        """
        Adding metadata after receiving results
        :return: dictionary containing metadata and results
        """
        date_now = datetime.now()
        date_time_now = date_now.strftime("%d/%m/%Y %H:%M:%S")
        payload_to_save = {}
        payload_to_save[self.config.api["nom_api"]] = pkg_resources.require("careerjet_api")[0].version
        payload_to_save["Version client python"] = platform.python_version()
        payload_to_save["HTML"] = job_offers
        payload_to_save["Source externe"] = self.config.api["nom_api"]
        payload_to_save["Date et heure"] = date_time_now
        payload_to_save["La langue"] = self.config.api["locale_code"]
        return payload_to_save

    def persistResult(self, payload_to_save):
        """
        Insert results in database(MongoDB)
        """
        x = self.collection.insert_one(payload_to_save)
        print("La requête vers l'api a bien été fait et la réponse a bien été enregistrer dans mongoDB!!")
