using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox.SpriteStudio.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SiliconStudio.Paradox.SpriteStudio.Offline
{
    internal class SpriteStudioXmlImport
    {
        private static void FillNodeData(SpriteStudioNode node, XNamespace nameSpace, XElement part, out SpriteNodeData nodeData)
        {
            nodeData = new SpriteNodeData();

            var attribs = part.Descendants(nameSpace + "Attribute");
            foreach (var attrib in attribs)
            {
                var tag = attrib.Attributes("Tag").First().Value;
                var keys = attrib.Descendants(nameSpace + "Key");

                var values = keys.Where(key => key.Descendants(nameSpace + "Value").FirstOrDefault() != null).Select(key => new Dictionary<string, string>
                        {
                            {"time", key.Attribute("Time").Value},
                            {"curve", key.Attribute("CurveType") != null ? key.Attribute("CurveType").Value : "0"},
                            {"value", key.Descendants(nameSpace + "Value").First().Value}
                        }).ToList();

                switch (tag)
                {
                    case "POSX":
                        node.BaseXyPrioAngle.X = Convert.ToSingle(values.Select(x => x.First(y => y.Key == "value").Value).First());
                        break;

                    case "POSY":
                        node.BaseXyPrioAngle.Y = -Convert.ToSingle(values.Select(x => x.First(y => y.Key == "value").Value).First());
                        break;

                    case "PRIO":
                        node.BaseXyPrioAngle.Z = Convert.ToSingle(values.Select(x => x.First(y => y.Key == "value").Value).First());
                        break;

                    case "ANGL":
                        node.BaseXyPrioAngle.W = MathUtil.DegreesToRadians(Convert.ToSingle(values.Select(x => x.First(y => y.Key == "value").Value).First()));
                        break;

                    case "SCAX":
                        node.BaseScale.X = Convert.ToSingle(values.Select(x => x.First(y => y.Key == "value").Value).First());
                        break;

                    case "SCAY":
                        node.BaseScale.Y = Convert.ToSingle(values.Select(x => x.First(y => y.Key == "value").Value).First());
                        break;

                    case "TRAN":
                        node.BaseTransparency = Convert.ToSingle(values.Select(x => x.First(y => y.Key == "value").Value).First());
                        break;

                    case "HIDE":
                        node.BaseHide = Convert.ToInt32(values.Select(x => x.First(y => y.Key == "value").Value).First()) == 1;
                        break;

                    case "FLPH":
                        node.HFlipped = Convert.ToInt32(values.Select(x => x.First(y => y.Key == "value").Value).First()) == 1;
                        break;

                    case "FLPV":
                        node.VFlipped = Convert.ToInt32(values.Select(x => x.First(y => y.Key == "value").Value).First()) == 1;
                        break;
                }

                nodeData.Data.Add(tag, values);
            }
        }

        public static bool Load(string file, List<SpriteStudioNode> nodes, List<SpriteNodeData> nodesData, out int endFrame, out int fps)
        {
            endFrame = 0;
            fps = 0;

            var xmlDoc = XDocument.Load(file);
            if (xmlDoc.Root == null) return false;

            var nameSpace = xmlDoc.Root.Name.Namespace;

            if (!int.TryParse(xmlDoc.Descendants(nameSpace + "EndFrame").First().Value, out endFrame)) return false;
            if (!int.TryParse(xmlDoc.Descendants(nameSpace + "BaseTickTime").First().Value, out fps)) return false;

            var parts = xmlDoc.Descendants(nameSpace + "Part").ToList();
            foreach (var part in parts)
            {
                var type = part.Descendants(nameSpace + "Type").First();
                var name = part.Descendants(nameSpace + "Name").First();
                switch (type.Value)
                {
                    case "1":
                        {
                            var node = new SpriteStudioNode { Name = name.Value, Id = -1, ParentId = -2 };
                            nodes.Add(node);
                            nodesData.Add(new SpriteNodeData());
                        }
                        break;

                    case "2":
                        {
                            int nodeId, parentId;
                            if (!int.TryParse(part.Descendants(nameSpace + "ID").First().Value, out nodeId)) continue;
                            if (!int.TryParse(part.Descendants(nameSpace + "ParentID").First().Value, out parentId)) continue;

                            var node = new SpriteStudioNode
                            {
                                Name = name.Value,
                                Id = nodeId,
                                ParentId = parentId,
                                PictureId = -1,
                                Rectangle = RectangleF.Empty,
                                Pivot = Vector2.Zero
                            };

                            nodes.Add(node);

                            SpriteNodeData nodeData;
                            FillNodeData(node, nameSpace, part, out nodeData);

                            nodesData.Add(nodeData);
                        }
                        break;

                    case "0":
                        {
                            int nodeId, parentId;
                            if (!int.TryParse(part.Descendants(nameSpace + "ID").First().Value, out nodeId)) continue;
                            if (!int.TryParse(part.Descendants(nameSpace + "ParentID").First().Value, out parentId)) continue;

                            int textureId;
                            if (!int.TryParse(part.Descendants(nameSpace + "PicID").First().Value, out textureId)) continue;
                            var pictAreaX = part.Descendants(nameSpace + "PictArea").First();
                            int top, left, bottom, right;
                            if (!int.TryParse(pictAreaX.Descendants(nameSpace + "Top").First().Value, out top)) continue;
                            if (!int.TryParse(pictAreaX.Descendants(nameSpace + "Left").First().Value, out left)) continue;
                            if (!int.TryParse(pictAreaX.Descendants(nameSpace + "Bottom").First().Value, out bottom)) continue;
                            if (!int.TryParse(pictAreaX.Descendants(nameSpace + "Right").First().Value, out right)) continue;
                            var rect = new RectangleF(left, top, right - left, bottom - top);

                            int pivotX, pivotY;
                            if (!int.TryParse(part.Descendants(nameSpace + "OriginX").First().Value, out pivotX)) continue;
                            if (!int.TryParse(part.Descendants(nameSpace + "OriginY").First().Value, out pivotY)) continue;

                            int blending;
                            if (!int.TryParse(part.Descendants(nameSpace + "TransBlendType").First().Value, out blending)) continue;

                            var node = new SpriteStudioNode
                            {
                                Name = name.Value,
                                Id = nodeId,
                                ParentId = parentId,
                                PictureId = textureId,
                                Rectangle = rect,
                                Pivot = new Vector2(pivotX, pivotY),
                                BaseAlphaBlending = (SpriteStudioAlphaBlending)blending
                            };

                            nodes.Add(node);

                            SpriteNodeData nodeData;
                            FillNodeData(node, nameSpace, part, out nodeData);

                            nodesData.Add(nodeData);
                        }
                        break;
                }
            }

            return true;
        }
    }
}