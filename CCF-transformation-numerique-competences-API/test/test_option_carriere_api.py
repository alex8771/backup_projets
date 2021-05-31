import platform
import socket
import time
import unittest
from datetime import datetime

import pkg_resources
import pymongo
from careerjet_api import CareerjetAPIClient
from omegaconf import OmegaConf

from client_python.OptionCarriereAPI import OptionCarriereAPI
from test.mock.option_carriere_api_mock import OptionCarriereAPIMock


class TestOptionCarriereAPI(unittest.TestCase):

    def setUp(self) -> None:
        # Initialize configuration
        cfg = dict({"api": dict()})
        cfg["api"]["keywords"] = ["assistant", "assurance"]
        cfg["api"]["location"] = ["Québec", "Montréal"]
        cfg["api"]["affid"] = "77e7b3bdafe9a368b0fe5a9f1024d466"
        cfg["api"]["user_agent"] = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0"
        cfg["api"]["url"] = "https://iid.ulaval.ca/"
        cfg["api"]["locale_code"] = "en_CA"
        cfg["api"]["nom_api"] = "Option-Carriere"
        cfg["api"]["database"] = "test"
        self.config = OmegaConf.create(cfg)

        # Initialize configuration with no keyword and one location
        cfg2 = dict({"api": dict()})
        cfg2["api"]["keywords"] = []
        cfg2["api"]["location"] = ["Québec"]
        cfg2["api"]["affid"] = "77e7b3bdafe9a368b0fe5a9f1024d466"
        cfg2["api"]["user_agent"] = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0"
        cfg2["api"]["url"] = "https://iid.ulaval.ca/"
        cfg2["api"]["locale_code"] = "en_CA"
        cfg2["api"]["nom_api"] = "Option-Carriere"
        cfg2["api"]["database"] = "test"
        self.config_nok_onel = OmegaConf.create(cfg2)

        # Initialize dictionary with keyword and location
        my_dict = dict()
        my_dict["keywords"] = "assistant assurance"
        my_dict["location"] = "Québec Montréal"
        my_dict["affid"] = "77e7b3bdafe9a368b0fe5a9f1024d466"
        my_dict["user_ip"] = socket.gethostbyname(socket.gethostname())
        my_dict["user_agent"] = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0"
        my_dict["url"] = "https://iid.ulaval.ca/"
        my_dict["locale_code"] = "en_CA"
        self.call_reference = my_dict

        # Initialize dictionary with no keyword and one location
        my_dict2 = dict()
        my_dict2["keywords"] = ""
        my_dict2["location"] = "Québec"
        my_dict2["affid"] = "77e7b3bdafe9a368b0fe5a9f1024d466"
        my_dict2["user_ip"] = socket.gethostbyname(socket.gethostname())
        my_dict2["user_agent"] = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0"
        my_dict2["url"] = "https://iid.ulaval.ca/"
        my_dict2["locale_code"] = "en_CA"
        self.call_reference_nok_onel = my_dict2

        # Initialize configuration for addMetadata and persistResult
        date_now = datetime.now()
        date_time_now = date_now.strftime("%d/%m/%Y %H:%M:%S")
        payload_to_save = {}
        payload_to_save[self.config.api["nom_api"]] = pkg_resources.require("careerjet_api")[0].version
        payload_to_save["Version client python"] = platform.python_version()
        payload_to_save["HTML"] = "offre emplois"
        payload_to_save["Source externe"] = self.config.api["nom_api"]
        payload_to_save["Date et heure"] = date_time_now
        payload_to_save["La langue"] = self.config.api["locale_code"]
        self.add_data = payload_to_save

    def test_init(self):
        """
        Initialize OptionCarriereAPIMock instance with config
        test initialization of the OptionCarriereAPI class
        success if the class is initialized correctly
        """
        job_offer_caller = OptionCarriereAPIMock(self.config)
        self.assertEqual(job_offer_caller.config, self.config)
        self.assertIsInstance(job_offer_caller, OptionCarriereAPI)
        self.assertDictEqual(job_offer_caller.call, self.call_reference)
        host_connection_mongoDB = pymongo.MongoClient("localhost:27017")
        self.assertEqual(job_offer_caller.connection, host_connection_mongoDB)
        database_mongoDB = host_connection_mongoDB[self.config.api["database"]]
        self.assertEqual(job_offer_caller.database, database_mongoDB)
        collection_mongoDB = database_mongoDB[self.config.api["nom_api"]]
        self.assertEqual(job_offer_caller.collection, collection_mongoDB)
        self.assertEqual(self.config.api["locale_code"], job_offer_caller.api.locale_code)
        self.assertIsInstance(job_offer_caller.api, CareerjetAPIClient)

    def test_init_config2(self):
        """
        Initialize OptionCarriereAPIMock instance with config
        test initialization of the OptionCarriereAPI class
        test with only no keyword and one location
        """
        job_offer_caller = OptionCarriereAPIMock(self.config_nok_onel)
        self.assertEqual(job_offer_caller.config, self.config_nok_onel)
        self.assertDictEqual(job_offer_caller.call, self.call_reference_nok_onel)

    def test_add_metadata(self):
        """
        Initialize OptionCarriereAPIMock instance with config
        test addMetadata method with string
        """
        job_offer_caller = OptionCarriereAPIMock(self.config)
        self.assertDictEqual(job_offer_caller.addMetadata("offre emplois"), self.add_data)

    def test_persit_result(self):
        """
        Initialize OptionCarriereAPIMock instance with config
        test data persistence in mongoDB
        Compare the data read in bd with the dictionary
        """
        job_offer_caller = OptionCarriereAPIMock(self.config)
        job_offer_caller.persistResult(self.add_data)
        collection = job_offer_caller.collection
        data = collection.find_one(self.add_data)
        self.assertDictEqual(data, self.add_data)

    def test_run(self):
        """
        Initialize OptionCarriereAPIMock instance with config
        Test run method
        Compare the data read in bd with the dictionary
        """
        time.sleep(1)
        job_offer_caller = OptionCarriereAPIMock(self.config)
        job_offer_caller.run()
        date_time_now = datetime.now().strftime("%d/%m/%Y %H:%M:%S")
        payload_to_save = {}
        payload_to_save[self.config.api["nom_api"]] = pkg_resources.require("careerjet_api")[0].version
        payload_to_save["Version client python"] = platform.python_version()
        payload_to_save["HTML"] = job_offer_caller.callExternalAPI()
        payload_to_save["Source externe"] = self.config.api["nom_api"]
        payload_to_save["Date et heure"] = date_time_now
        payload_to_save["La langue"] = self.config.api["locale_code"]
        data = {}
        seach_data = {"Option-Carriere": "{0}".format(pkg_resources.require("careerjet_api")[0].version),
                      "Version client python": "{0}".format(platform.python_version()),
                      "Source externe": "{0}".format(self.config.api["nom_api"]),
                      "Date et heure": "{0}".format(date_time_now)}
        data = job_offer_caller.collection.find_one(seach_data, {"_id": 0})
        self.assertDictEqual(data, payload_to_save)
