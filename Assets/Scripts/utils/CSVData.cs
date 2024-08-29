using Excel;
using System.Data;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Reflection;
using System;

namespace CSV
{
    public class CSVData
    {
        public DataTable dt;
        public int columnCount = 0;
        public void OpenCSV(string filePath)//从csv读取数据返回table
        {
            dt = new DataTable();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    //记录每次读取的一行记录
                    string strLine = "";
                    //记录每行记录中的各字段内容
                    string[] aryLine = null;
                    string[] tableHead = null;
                    //标示列数
                    columnCount = 0;
                    //标示是否是读取的第一行
                    bool IsFirst = true;
                    //逐行读取CSV中的数据
                    while ((strLine = sr.ReadLine()) != null)
                    {
                        if (IsFirst == true)
                        {
                            tableHead = strLine.Split(',');
                            IsFirst = false;
                            columnCount = tableHead.Length;
                            Debug.Log(filePath + " " + columnCount);
                            //创建列
                            for (int i = 0; i < columnCount; i++)
                            {
                                DataColumn dc = new DataColumn(tableHead[i]);
                                dt.Columns.Add(dc);
                                //Debug.Log("in datacol "+dc.ToString());
                            }
                        }
                        else
                        {
                            aryLine = strLine.Split(',');
                            DataRow dr = dt.NewRow();
                            for (int j = 0; j < columnCount; j++)
                            {
                                dr[j] = aryLine[j];
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                    if (aryLine != null && aryLine.Length > 0)
                    {
                        dt.DefaultView.Sort = tableHead[0] + " " + "asc";
                    }
                    sr.Close();
                    fs.Close();
                }
            }
        }

        public object get(int rowindex, string name)
        {
            int j = -1;
            for (int k = 0; k < columnCount; k++)
            {
                //Debug.Log("in_get " + dt.Columns[k].ToString());
                if (dt.Columns[k].ToString() == name)
                {
                    j = k;
                    break;
                }
            }
            if (j == -1)
            {
                //Debug.Log("cant find " + name);
                return null;
            }
            else
            {
                //Debug.Log("find " + name + " value is " + dt.Rows[rowindex][j].ToString());
                return dt.Rows[rowindex][j];
            }
        }
        public object get(string ID, string name)
        {
            int i = -1, j = -1;
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                if (dt.Rows[k][0].ToString() == ID)
                {
                    i = k;
                    break;
                }
            }
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                if (dt.Columns[k].ToString() == name)
                {
                    j = k;
                    break;
                }
            }
            if (i == -1 || j == -1)
            {
                return null;
            }
            else
            {

                return dt.Rows[i][j];
            }
        }

        public T DatafromCSV<T>(string ID)
        {
            Type type = typeof(T);
            PropertyInfo[] properties= type.GetProperties();
            object[] args = new object[properties.Length];
            for (int i=0; i<properties.Length; i++)
            {
                //Debug.Log("test order:  " + properties[i].Name);
                if (properties[i].PropertyType == typeof(string))
                {
                    args[i] = Convert.ToString(get(ID, properties[i].Name));
                }
                else if (properties[i].PropertyType == typeof(int))
                {
                    args[i] = Convert.ToInt32(get(ID, properties[i].Name));
                }
                else if (properties[i].PropertyType == typeof(float))
                {
                    args[i] = Convert.ToSingle(get(ID, properties[i].Name));
                }
                else
                {
                    Debug.Log("数据类型超出处理范围");
                }
            }
            T obj = (T)Activator.CreateInstance(type,args);
            return obj;
        }

        public T DatafromCSV<T>(int rowindex)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            object[] args = new object[properties.Length];
            //Debug.Log("参数长度：" + args.Length);
            for (int i = 0; i < properties.Length; i++)
            {
                //Debug.Log("test order:  " + properties[i].Name);
                //Debug.Log("this property type is "+properties[i].PropertyType);
                if(properties[i].PropertyType == typeof(string))
                {
                    args[i] = Convert.ToString(get(rowindex, properties[i].Name));
                }
                else if(properties[i].PropertyType == typeof(int))
                {
                    args[i] = Convert.ToInt32(get(rowindex, properties[i].Name));
                }
                else if (properties[i].PropertyType == typeof(float))
                {
                    args[i] = Convert.ToSingle(get(rowindex, properties[i].Name));
                }
                else
                {
                    Debug.Log("数据类型超出处理范围");
                }
                //Debug.Log("is " + Convert.ToString(args[i]));
                //list.Add(get(rowindex, properties[i].Name));
            }
            
            T obj = (T)Activator.CreateInstance(type, args);
            return obj;
        }

        public int getRowsNum()
        {
            return dt.Rows.Count;
    }
    }

    
}

