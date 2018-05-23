# !/usr/bin/env python3
# -*- coding: utf-8 -*-
# create data : 2018/05/14
# author : Zhiquan.Wang

import logging
import socket
import threading


class ServerCommunication(object):
    def __init__(self, _ip, _p, _s_manager):
        logging.basicConfig(filename='server_log.log', format='%(asctime)s - %(levelname)s - %(message)s',
                            level=logging.INFO)
        self.__host_ip = _ip
        self.__port = _p
        self.__socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.__socket.bind((self.__host_ip, self.__port))
        self.__max_connect_num = 512
        self.__ref_ser_manager = _s_manager

    def launch_server(self):
        try:
            self.__socket.listen(self.__max_connect_num)
            while True:
                client_socket_info = self.__socket.accept()
                address_thread = threading.Thread(target=self.__address_request, args=(client_socket_info,))
                address_thread.start()
        except Exception:
            logging.exception('Launch Server System Failed')

    def __address_request(self, _c_socket_info):
        client_socket = _c_socket_info[0]
        try:
            # Receive Client Message
            while True:
                data = client_socket.recv(2048)
                if not data:
                    break
                msg_info = data.decode('utf-8')
                print("MSG : ",msg_info)
                server_reply = self.__ref_ser_manager.address_request(msg_info)
                client_socket.sendall(server_reply.encode('utf-8'))
            client_socket.close()
        except Exception:
            logging.exception('Class:ServerNetwork:address_request')
            client_socket.close()
