from flask import Flask, request, jsonify
import time
from llamalogic import *
from fuzzylogic import *

app = Flask(__name__)


@app.route('/fuzzyapi', methods=['GET'])
#  [0] AngerXFear [Range(-1f, 1f)]
#  [1] DisgustXTrust [Range(-1f, 1f)]
#  [2] SadnessXJoy [Range(-1f, 1f)]
#  [3] AntecipationXSurprise [Range(-1f, 1f)]
def get():
    emo = getEmotion()
    return {"emotion" : emo}

@app.route('/fuzzyapi', methods=['POST'])
def post():
    emo = []
    emo.append(request.form['axeAF'])
    emo.append(request.form['axeDT'])
    emo.append(request.form['axeSJ'])
    emo.append(request.form['axeAS'])
    for i in range(4):
        emo[i] = emo[i].replace(",",".")
        emo[i] = float(emo[i])
    postEmotion(emo)
    return '', 204 
    # return request.json();
# /<float:axeAF>/<float:axeDT>/<float:axeSJ>/<float:axeAS>


###########################
@app.route('/llamaapi', methods=['POST'])
def generate():
    data = request.get_json()
    # print(data)
    prompt = data.get("prompt", "")
    # Optional: allow overriding max_tokens from Unity later
    # max_tokens = data.get("max_tokens", 150)
    
    output = generateResponse(prompt)
    return jsonify(output)
##############################

if __name__ == "__main__":
    app.run(port=11434)
