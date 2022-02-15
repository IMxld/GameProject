using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JsonWriteTest : MonoBehaviour
{
    private string fileName;    // ����һ��string���͵ı��� ���ļ�����

����  private string path;        //�����и�string���͵ı���������·������

����  void Start()

    {

        path = Application.dataPath + "/Resources";      //��������ֵָ��·��

        fileName = "Student.json";                             //��ֵ��

        if (!Directory.Exists(path))                                //�ж�·���Ƿ���ڲ����ھʹ���һ����     

        {

            Directory.CreateDirectory(path);

        }

        fileName = Path.Combine(path, fileName);     //���ļ�����·���ϲ�

        if (!File.Exists(fileName))     //�ж��� �Ƿ��Ѿ����ڲ����ھʹ���һ���ļ���

        {

            FileStream fs = File.Create(fileName);

            fs.Close();

        }

        Saves();      // �����ļ��ķ�����     

        Read();    // ��ȡ�ļ��ķ�����

    }

    void Saves()

    {
        string json = JsonUtility.ToJson(PersonContainer.Instace);

        File.WriteAllText(fileName, json, Encoding.UTF8);    //utf8 �����������룻 

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
