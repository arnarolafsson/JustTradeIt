import json
from services.emailService import send_simple_message

email_header = '<h2>Hello %s </h2><p>The trade request between you and %s has been <em>%s</em>!</p><br>Trade summary:'
sender_info = '<br><h3>Info about trader:</h3><h4>Name: %s</h4><h4>Email: <a href="#">%s</a></h4><img src="%s">'
trade_info_table= '<table><thead><tr style="background-color: rgba(155, 155, 155, .2)"></tr><th>Issue Date</th><th>Modification Date</th><th>Modified By</th><th>Trade Status</th></thead><tbody><tr><td>%s</td><td>%s</td><td>%s</td><td>%s</td></tr></tbody></table><br>'
email_table = '<br><table><thead><tr style="background-color: rgba(155, 155, 155, .2)"><th>Title</th><th>Description</th></tr></thead><tbody>%s</tbody></table>'
def tradeStatus(status):
    if(status == 1):
        return 'Accepted'
    elif(status == 2):
        return 'Declined'
    elif(status == 3):
        return 'Cancelled'
    elif(status == 4):
        return 'Timed out'

def send_trade_update_email(ch, method, properties, body):
    parsed_data = json.loads(body)
    receiver = parsed_data['Receiver']
    receiver_email = receiver['Email']
    sender = parsed_data['Sender']
    subject = "Trade offer update"
    status = tradeStatus(int(parsed_data['Status']))

    header = email_header % (receiver["FullName"], sender["FullName"], status)

    rec_items = parsed_data['ReceivingItems']
    offering_items = parsed_data['OfferingItems']

    rec_items_html = ''.join([ '<tr><td>%s</td><td>%s</td></tr>' % (item['Title'], item['ShortDescription']) for item in rec_items ])
    offering_items_html = ''.join([ '<tr><td>%s</td><td>%s</td></tr>' % (item['Title'], item['ShortDescription']) for item in offering_items ])
    
    rec_table = sender['FullName'] + ' Wants:' + (email_table % rec_items_html)
    off_table = 'In return for:' + (email_table % offering_items_html)
    update_info = trade_info_table % (parsed_data["IssuedDate"], parsed_data["ModifiedDate"], parsed_data["ModifiedBy"], status)
    trader = sender_info % (sender['FullName'], sender['Email'], sender["ProfileImageUrl"])

    body_message = header + update_info + rec_table + off_table + trader
    send_simple_message(receiver_email,subject,body_message)


def setup_handler(channel, exchange_name):
    trade_update_routing_key = 'trade-update-request'
    trade_update_queue = 'trade-update-queue'
    # Declare the queue, if it doesn't exist
    channel.queue_declare(queue=trade_update_queue, durable=True)
    # Bind the queue to a specific exchange with a routing key
    channel.queue_bind(exchange=exchange_name, queue=trade_update_queue, routing_key=trade_update_routing_key)

    channel.basic_consume(queue=trade_update_queue,
                      on_message_callback=send_trade_update_email,
                      auto_ack=True)
