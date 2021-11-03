import requests

def send_simple_message(to, subject, body):
    return requests.post(
        "https://api.mailgun.net/v3/sandbox85f86b8828004106b5515f67cca41de1.mailgun.org/messages",
        auth=("api", "d1c6450ff62aa712e52a1624402f401a-2ac825a1-dfa6d528"),
        data={"from": "Mailgun Sandbox <postmaster@sandbox85f86b8828004106b5515f67cca41de1.mailgun.org>",
              "to": to,
              "subject": subject,
              "html": body})