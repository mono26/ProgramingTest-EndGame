using System;
using System.Text;
using UnityEngine;

namespace EndGame.Test.Utils
{
    public static class ResourcesUtils
    {
        public static GameObject GetResource(string _resourceId)
        {
            return GetResourceGameObject(_resourceId, null);
        }

        public static GameObject GetResourceGameObject(string _resourceId, string _subFolder)
        {
            GameObject resourceToReturn;
            try
            {
                string path = GetPath(_subFolder, _resourceId);
                resourceToReturn =  Resources.Load<GameObject>($"{ _subFolder }/{ _resourceId }");
            }
            catch (Exception _exception)
            {
                // TODO throw no resource found exception.
                throw new Exception($"Can't find a resource: { _exception.Message }", _exception);
            }
            return resourceToReturn;
        }

        public static string GetPath(string _subFolder, string _id)
        {
            StringBuilder path = new StringBuilder();
            if (_subFolder.HasAValue())
            {
                path.Append($"{ _subFolder }/");
            }
            if (_id.HasAValue())
            {
                path.Append(_id);
                return path.ToString();
            }
            else
            {
                // TODO throw can't get path of null id.
                throw new Exception("Can't get path of a null id.");
            }
        }
    }
}
