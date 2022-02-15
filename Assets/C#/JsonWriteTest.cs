using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JsonWriteTest : MonoBehaviour
{
    private string fileName;    // 定义一个string类型的变量 （文件名）

　　  private string path;        //定义有个string类型的变量（创建路径名）

　　  void Start()

    {

        path = Application.dataPath + "/Resources";      //给变量赋值指定路径

        fileName = "Student.json";                             //赋值名

        if (!Directory.Exists(path))                                //判断路径是否存在不存在就创建一个；     

        {

            Directory.CreateDirectory(path);

        }

        fileName = Path.Combine(path, fileName);     //将文件名和路径合并

        if (!File.Exists(fileName))     //判断文 是否已经存在不存在就创建一个文件；

        {

            FileStream fs = File.Create(fileName);

            fs.Close();

        }

        Saves();      // 保存文件的方法；     

        Read();    // 读取文件的方法；

    }

    void Saves()

    {
        string json = JsonUtility.ToJson(PersonContainer.Instace);

        File.WriteAllText(fileName, json, Encoding.UTF8);    //utf8 万国码避免乱码； 

    }

    void Read()

    {

        string json = File.ReadAllText(fileName, Encoding.UTF8);

        PersonContainer.Instace = JsonUtility.FromJson<PersonContainer>(json);

        for (int i = 0; i < PersonContainer.Instace.personList.Count; i++)

        {

            Debug.Log(PersonContainer.Instace.personList[i]);

        }

    }

    void Update() { }



}

[System.Serializable]

public class PersonContainer

{

    public List<Person> personList;

    private static PersonContainer instance;

    public static PersonContainer Instace

    {

        get
        {
            if (instance == null)

            {

                instance = new PersonContainer();

            }

            return instance;

        }

        set
        {
            instance = value;

        }

    }

    public PersonContainer()

    {

        personList = new List<Person>();

        personList.Add(new Person(("zhangsan"), 10));

        personList.Add(new Person("lisi", 11));

    }

}

[System.Serializable]

public class Person

{
    public string name; 
    
    public int age;

    public Person() { }

    public Person(string name, int age)

    {
        this.name = name;

        this.age = age;

    }

    public override string ToString()

    {

        return this.name + "," + this.age;

    }

}
