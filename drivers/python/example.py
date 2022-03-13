import BarbarisDriver as bdb
# pip install -U requests

# dict with fields 
data = {
    "method": "get",                # method get/set (diferences are obvious)
    "file": "testfile",             # name of database file
    "data": "name=gleb and id=4"    # request string / data string (depends on method, for more info see readme file)
}

print(bdb.bdb_request("http://localhost:5232/", data))  # calling for bdb_request function form BarbarisDriver and putting db's url and dict as params
