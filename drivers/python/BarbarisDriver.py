from urllib import response
import requests

def bdb_request(url, data):
    response = requests.get(url, json=data)
    return response.json()["output"]

