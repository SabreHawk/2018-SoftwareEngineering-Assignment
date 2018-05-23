# !/usr/bin/env python3
# -*- coding: utf-8 -*-
# create data : 2018/05/14
# author : Zhiquan.Wang

import ServerCommunication
import ServiceManager
import DbManager

if __name__ == '__main__':
    test_db = DbManager.DbManager()
    test_serverManager = ServiceManager.ServiceManager(test_db)
    test_server = ServerCommunication.ServerCommunication('127.0.0.1', 8829, test_serverManager)
    test_server.launch_server()
