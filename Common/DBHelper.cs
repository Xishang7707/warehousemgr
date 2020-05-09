﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DBHelper
    {
        private const string conn_str = @"Database=warehousemgr;Data Source=8.129.167.212;User Id=sa;Password=123456";
        private IDbConnection conn;
        private IDbTransaction tran;
        public DBHelper()
        {
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public void Connect()
        {
            if (conn == null || conn.State != System.Data.ConnectionState.Open && conn.State != System.Data.ConnectionState.Executing && conn.State != System.Data.ConnectionState.Fetching)
            {
                conn = new SqlConnection(conn_str);
                conn.Open();
            }
        }

        /// <summary>
        /// 断开
        /// </summary>
        /// <returns></returns>
        public void Close()
        {
            if (conn != null && conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public void BeginTransaction()
        {
            Connect();
            tran = conn.BeginTransaction();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        public void Commit()
        {
            tran.Commit();
            tran = null;
        }

        /// <summary>
        /// 回滚
        /// </summary>
        /// <returns></returns>
        public void Rollback()
        {
            if (tran != null)
                tran.Rollback();
            tran = null;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public async Task<List<T>> QueryListAsync<T>(string sql, object param = null)
        {
            Connect();
            return (await conn.QueryAsync<T>(sql, param, tran)).ToList();
        }

        /// <summary>
        /// 查询单条
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">SQL</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public async Task<T> QueryAsync<T>(string sql, object param = null)
        {
            Connect();
            return await conn.QueryFirstOrDefaultAsync<T>(sql, param, tran);
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<T> ExecAsync<T>(string sql, object param)
        {
            Connect();
            return await conn.ExecuteScalarAsync<T>(sql, param, tran);
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> ExecAsync(string sql, object param)
        {
            Connect();
            return await conn.ExecuteAsync(sql, param, tran);
        }
    }
}
