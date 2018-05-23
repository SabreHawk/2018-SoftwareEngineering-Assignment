# !/usr/bin/env python3
# -*- coding: utf-8 -*-
# create data : 2018/05/14
# author : Zhiquan.Wang


import logging
import DbManager


class ServiceManager(object):

    def __init__(self, _db_manager):
        self.__ref_db_manager = _db_manager

    def address_request(self, _client_request):
        try:
            msg_element = _client_request.split(':')
            msg_type = msg_element[0]
            # Manage Request
            if msg_type == 'QueryResults':
                return self.__address_query_results()
            elif msg_type == 'UploadResult':
                return self.__address_upload_result(msg_element[1], msg_element[2])
            else:
                return 'False:illegal message type'
        except Exception:
            logging.exception('Class:ServiceManager:address_request')
            return 'False:Exception'

    def __address_query_results(self):
        try:
            tmp_res = self.__ref_db_manager.query_results()
            tmp_msg = 'True:'
            for msg in tmp_res[1]:
                tmp_msg += str(msg)+'|'
            print(tmp_msg)
            return tmp_msg
        except Exception:
            logging.exception('Class:ServiceManager:address_query_results')
            return False, 'False:Exception'

    def __address_upload_result(self, _n, _s):
        try:
            self.__ref_db_manager.insert_result(_n, _s)
            return True
        except Exception:
            logging.exception('Class:ServiceManager:address_upload_result')
            return False, 'False:Exception'
