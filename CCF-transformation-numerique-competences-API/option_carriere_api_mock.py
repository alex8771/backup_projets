from client_python.OptionCarriereAPI import OptionCarriereAPI
import pymongo

class OptionCarriereAPIMock(OptionCarriereAPI):

    def callExternalAPI(self):
        response = []

        element_one = dict()
        element_one["location"] = "Québec"
        element_one["Date"] = "Mon, 29 Jun 2020 07:49:39 GMT"
        element_one["url"] = "https://localhost/1"
        element_one["title"] = "Titre de l'offre 1"
        element_one["description"] = "Description de l'offre 1"
        element_one["company"] = "Compagnie de l'offre 1"
        element_one["salary"] = "Salaire de l'offre 1"

        element_two = dict()
        element_two["location"] = "Montréal"
        element_two["Date"] = "Mon, 29 Jun 2020 08:49:39 GMT"
        element_two["url"] = "https://localhost/2"
        element_two["title"] = "Titre de l'offre 2"
        element_two["description"] = "Description de l'offre 2"
        element_two["company"] = "Compagnie de l'offre 2"
        element_two["salary"] = "Salaire de l'offre 2"

        response.append(element_one)
        response.append(element_two)

        return response
