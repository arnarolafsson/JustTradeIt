import json
from services.emailService import send_simple_message
email_header = '<h2>Hello %s </h2><p> %s (<a>%s</a>)wants to make a trade!</p>'
email_table = '<br><table><thead><tr style="background-color: rgba(155, 155, 155, .2)"><th>Title</th><th>Description</th></tr></thead><tbody>%s</tbody></table>'
end_msg = '<h3>Navigate to <a href="#">JustTradeIt</a> to respond!</h3>'

def send_trade_request_email(ch, method, properties, body):
    parsed_data = json.loads(body)
    receiver = parsed_data['Receiver']
    receiver_email = receiver['Email']
    sender = parsed_data['Sender']
    subject = "New Trade Request From: " + sender['FullName']

    rec_items = parsed_data['ReceivingItems']
    offering_items = parsed_data['OfferingItems']

    rec_items_html = ''.join([ '<tr><td>%s</td><td>%s</td></tr>' % (item['Title'], item['ShortDescription']) for item in rec_items ])
    offering_items_html = ''.join([ '<tr><td>%s</td><td>%s</td></tr>' % (item['Title'], item['ShortDescription']) for item in offering_items ])
    
    rec_table = sender['FullName'] + ' Wants:' + (email_table % rec_items_html)
    off_table = 'In return for:' + (email_table % offering_items_html)
    header = email_header % (receiver["FullName"], sender["FullName"], sender["Email"])
    body_message = header + rec_table + off_table + end_msg
    send_simple_message(receiver_email,subject,body_message)

def setup_handler(channel, exchange_name):
    new_trade_routing_key = 'new-trade-request'
    new_trade_queue = 'new-trade-queue'
    # Declare the queue, if it doesn't exist
    channel.queue_declare(queue=new_trade_queue, durable=True)
    # Bind the queue to a specific exchange with a routing key
    channel.queue_bind(exchange=exchange_name, queue=new_trade_queue, routing_key=new_trade_routing_key)

    channel.basic_consume(queue=new_trade_queue,
                      on_message_callback=send_trade_request_email,
                      auto_ack=True)