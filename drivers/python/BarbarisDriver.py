from urllib import response
import requests

def bdb_request(url, data):
    if data_test(url, data):
        response = requests.get(url, json=data)

        if response.json()["type"] == "error":
            print("Error: " + response.json()["output"])
            return "There is an error in your request. Check the log in terminal."
        elif response.json()["output"].startswith("Error:"):
            print(response.json()["output"])
            return "There is an error in your request. Check the log in terminal."
        else:
            return response.json()["output"]


def data_test(url, data):
    if url.startswith("http://") or url.startswith("https://"):
        if data.method == "set":
            if data.data.endswith(";"):
                return True
            else:
                print("Data error: Data string does not end with semicolon.")
                return False
        elif data.method == "get":
            return True
        else:
            print("Method error: Method is not \"get\" or \"set\", try to check the case.")
            return False
    else:
        print("Host error: HTTP or HTTPS did not find.")
        return False