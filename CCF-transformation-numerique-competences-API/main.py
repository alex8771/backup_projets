import hydra
from client_python.OptionCarriereAPI import OptionCarriereAPI
from omegaconf import DictConfig


@hydra.main(config_path='config/config.yaml')
def my_app(cfg: DictConfig) -> None:
    """
    Call run method of OptionCarriereAPI
    """
    job_api = OptionCarriereAPI(cfg)
    job_api.run()

if __name__ == "__main__":
    my_app()
