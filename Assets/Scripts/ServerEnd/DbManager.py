# !/usr/bin/env python3
# -*- coding: utf-8 -*-
# create data : 2018/05/14
# author : Zhiquan.Wang

import pymysql


class DbManager(object):
    def __init__(self):
        self.__db_ip = 'localhost'
        self.__user_name = 'root'
        self.__user_pwd = '0000'
        self.__db_name = 'se_manager'
        self.__db = pymysql.connect(self.__db_ip, self.__user_name, self.__user_pwd, self.__db_name)
        self.__cursor = self.__db.cursor()

    def close(self):
        self.__db.close()

    def __execute(self, _sql):
        if isinstance(_sql, list):
            for tmp_sql in _sql:
                try:
                    self.__cursor.execute(str(tmp_sql))
                    self.__db.commit()
                    return True
                except pymysql.DatabaseError as error:
                    self.__db.rollback()
                    return False
        elif isinstance(_sql, str):
            try:
                self.__cursor.execute(str(_sql))
                self.__db.commit()
                return True
            except pymysql.DatabaseError as error:
                self.__db.rollback()
                print(error)
                return False
        else:
            print("Error : Class DbManager Error > execute() - params must be a list or string")
            return False

    def __query(self, _sql):
        if isinstance(_sql, str):
            try:
                self.__cursor.execute(str(_sql))
                self.__db.commit()
                res = self.__cursor.fetchall()
                return res
            except pymysql.DatabaseError as error:
                print(error)
                return False
        else:
            print("Error : Class DbManager Error > execute")
            return False

    def query_results(self):  # _user_name : string
        query_sql = "select player_name,player_score from result_info order by player_score DESC limit 10 "
        tmp_res = self.__query(query_sql)
        return True, tmp_res

    def insert_result(self, _player_name, _player_score):
        print(_player_name)
        print(_player_score)
        insert_sql = """insert into result_info (player_name,player_score) VALUES ('%s', %s)""" % (
            _player_name, _player_score)
        self.__execute(insert_sql)
        return True
