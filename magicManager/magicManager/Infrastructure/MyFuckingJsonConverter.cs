using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Reflection;
using magicManager.Models;
namespace magicManager.Infrastructure
{
    public class MyFuckingJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
        //Ne s'en servir qu'en cas de besoin
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //1 - récupération de l'objet et instanciation 
            var MyObject = objectType.GetConstructors()[0].Invoke(null);
            //2- création de deux variables repères pour vérifier dans quel noeud on se situe
            bool isWebsite = false;
            bool isName = false;
            
            //3- déclaration d'une property info pour stocker la propriété au nom correspondant dans le fichier json
            PropertyInfo pi=null;
            //4- déclaration d'un objet permettant de stocker l'instanciation d'un objet de type classe pour l'objet à construire
            object PropClass = null;

            //5-début du read, on saute le premier terme car on sait de quel type d'objet provient le Json qu'on est en train de parser
            //reader.Read();
            //6-read jusqu'à al fin du Json
            while(reader.Read())
            {
                //6-1 Ici on va tenter d'ignorer les langues dont on n'a pas besoin, à cause de leur dénomination faite par des branques
                while (isName==true && isWebsite ==false && reader.Read())
                {
                    if (reader.Value != null)
                    {
                        if (reader.Value.ToString() == "website")
                            isWebsite = true;
                    }
                }
                //6-2 Vérif de ce qu'on a récupéré
                if (reader.Value != null)
                {
                   // MAGIIIIIIIIIIIE
                   //On va récupérer l'ensemble des propriétés de l'objet et les filtrer selon la clef récupérée dans le Json
                   //Si ce n'est pas une propriété, c'est une valeur --> voir le else
                    if (MyObject.GetType().GetProperties().Where(p => p.Name == reader.Value.ToString()).Count() > 0)
                    {
                        //ici on récup les infos de la propriété correspondant à la clef récupérée dans le Json (celle triée un peu plus haut)
                        pi = MyObject.GetType().GetProperty(reader.Value.ToString());
                        //Si la propriété-objet est une classe qui n'est pas un string, on l'instancie dans "propclass"
                        if(pi.PropertyType.IsClass && pi.PropertyType.Name != "String")
                        {
                           //on fait appel au tout premier constructeur de cette classe, à savoir celui qui a le moins d'arguments. (et qui perd donc tous ses débats)
                            PropClass = pi.PropertyType.GetConstructors()[0].Invoke(null);

                        }
                    }
                    //Si ce n'est pas une propriété c'est une valeur, que nous allons affecter à "PI"
                    else
                    {
                        //Si PropClass n'est pas instancié, c'est qu'il s'agit d'une propriété classique (int, bool, ...)
                        if (PropClass == null)
                        {
                            var val = reader.Value;
                            if (reader.ValueType.FullName == "System.Int64")
                            {
                                val = Convert.ToInt32(val);
                            }
                            //Permet d'affecter la valeur récupérée dans le Json à l'objet MyObject instancié au début
                            pi.SetValue(MyObject, val);
                        }
                        //Autrement, c'est une propriété de type classe 
                        else
                        {
                            //Si cette propriété s'appelle "Name"; sa première sous-propriété étant dans notre cas un objet intitulé "1", ce qui est impossible en c# 
                            if (PropClass.GetType().Name == "Name")
                            {
                                //On va donc aller chercher les propriétés de l'objet "name"
                                PropertyInfo[] piObj = PropClass.GetType().GetProperties();
                                //celles ci ne sont pas forcément dans le même ordre que l'objet généré à partir du Json; vu l'encodage de branques
                                //Donc au lieu d'un foreach, on fait un for; qui va s'appliquer à tout le contenu. Et pi c'est marre.
                                for (int i = 0; i <= piObj.Count(); i++)
                                {
                                    //On read pour sauter le "1" en question impossible à parser
                                    reader.Read();

                                    //Puis, si la valeur est différente de "null"
                                    if (reader.Value != null)
                                    {
                                        //on va chercher la valeur de la propriété en cours, et la stocker dans "CurrentProp"
                                        PropertyInfo CurrentProp = PropClass.GetType().GetProperty(reader.Value.ToString());
                                        //Puis on va read pour récupérer la valeur à mettre dans CurrentProp
                                        reader.Read();
                                        CurrentProp.SetValue(PropClass, reader.Value);
                                    }
                                }
                                //Une fois la propriété instanciée, on l'attribue à l'objet principal
                                pi.SetValue(MyObject, PropClass);
                                //On set à "true" le bool "isname" pour pouvoir sortir de la boucle et ignorer les autres langues (on ne prend donc que la première langue, l'anglais dans le cas présent)
                                isName = true;
                            }
                            else
                            {
                                //Si cette propriété ne s'appelle pas NAME, il faudra la traiter de la même façon, sans avoir besoin d'ignorer le "1"
                                PropertyInfo[] piObj = PropClass.GetType().GetProperties();
                                //Du coup il suffit de lire avec le même for que précédemment
                                for (int i = 0; i < piObj.Length; i++)
                                {
                               
                                    //Si le reader n'est pas nul, 
                                    if (reader.Value != null)
                                    {
                                        //On assigne à currentprop la valeur de la propriété
                                        PropertyInfo CurrentProp = PropClass.GetType().GetProperty(reader.Value.ToString());
                                        
                                        reader.Read();
                                        CurrentProp.SetValue(PropClass, reader.Value);
                                        //Attention! Lorsqu'on arrive à la dernière propriété, il n'est plus possible de boucler, 
                                        //cette ligne permet donc de s'assurer qu'il s'arrête à la dernière ligne et ne boucle plus ensuite
                                        if(i<piObj.Length-1) reader.Read();
                                    }
                                }
                                pi.SetValue(MyObject, PropClass);
                                
                            }
                            PropClass = null;
                        }
                    }

                }
            }

            return MyObject;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}