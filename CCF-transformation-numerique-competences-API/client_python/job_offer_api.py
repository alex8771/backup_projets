class JobOfferAPI:

    def __init__(self, config):
        """
        Initialized JobOfferAPI
        :param config: dictionary containing configure
        """
        # Initialize configuration
        self.config = config

    def callExternalAPI(self):
        """
        Call the external API
        :return: result of the external API class as dictionary
        """
        pass

    def addMetadata(self, job_offers):
        """
        Adding metadata after receiving results
        :return: dictionary containing metadata and results
        """
        pass

    def persistResult(self, payload_to_save):
        """
        persists results in database
        :return:
        """

    def run(self):
        # Call External APÎ
        response = self.callExternalAPI()
        # Adding metadata to result
        payload_to_save = self.addMetadata(response)
        # persists result
        self.persistResult(payload_to_save)
