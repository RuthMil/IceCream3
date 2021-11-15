﻿using IceCream3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IceCream3.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IceCream3Context context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Menu.Any())
            {
                return;   // DB has been seeded
            }

            var menus = new Menu[]
            {
            new Menu{Flavor="Chocolate",Price=12, Description="Ohhh that's good!", ImageUrl="data:image/jpeg;base64,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", DateAdded=DateTime.Parse("2021-10-17")},
            new Menu{Flavor="Strawberry", Price=19, Description="Strawberries are the best:)", ImageUrl="data:image/jpeg;base64,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", DateAdded=DateTime.Parse("2021-10-18")},
            new Menu{Flavor="Vanilla", Price=14, Description="The best Italian vanilla ice cream ever", ImageUrl="data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAoHCBUVFRgVEhUYGBgSHBUcGhgZEhgYGBIaGBgZGRoYGBgcIS4lHB4rIRgcJjgmKy8xNTU1HCQ7QDs0Py40NTEBDAwMEA8QHxISHjQrJCw0NDQxNDQ0NDQ0NjE0NjQ0NDQ9NDQ0NDQ0NDQ0NDQ0NDQ2NDQ0ND00NDQ0NDQ0NDQ0NP/AABEIARMAtwMBIgACEQEDEQH/xAAbAAABBQEBAAAAAAAAAAAAAAAAAQIDBAUGB//EADoQAAEDAgQEBAQFBAAHAQAAAAEAAhEDIQQSMUEFUWFxEyKBoQYykfAUQrHB0VJicvEjJEOCkqLhB//EABkBAQADAQEAAAAAAAAAAAAAAAABAgMEBf/EACURAAICAgIBBAIDAAAAAAAAAAABAhEDMRIhQQQyUWETIqHB0f/aAAwDAQACEQMRAD8A9DQEIUgclBTQlSgKlBTZSSgHyiUyU5ALKUFMlLKkD5Syo5RKgEmZKHKKUsoCWUSosyMyWCXMiVHKWUsEkolRSiUsEspJURekL0sE2ZCgzpUsEcpQmSiUBJKJUcougHlyaXJhlJlKAklKHKPKUoYVIJMyXMmeGU7w0AuZGZApIFNRQDMjMl8JHhJQELkmZO8FL4KUBmZGZPFFO8FKIsjzJMyl8BL4KULIC5MLla8BJ+HShZUzJVa/DoShYCilFFWwxLlUgq+EjwVbyIhAVfBR4KtQiEBW8FOFJWIRCAg8NL4amhKAgIRTS+EpoUGKxTWCTc8gqykoq2ErF8JKKapUOLNJhwjrMrRY8HQg+qpDLCemS4tbGeGl8NSIWpBH4aBTUiJQDPDRkUkolAR5EuVPlCAZlQnoQESEIQAEspEIByE1KCgFQhVsTjWM+Yqspxirk6QSb0WVG6s0dVi1OJPfJpkZRrP8qajVDyQDc5TA0DeY9SuR+si3UTT8TWzQGJJ2gDUlZGPxQfBF4meesJ2KxpcSxgMAQDzN7rMZiXh1+o+Qx9VjLN+VdPo0jHiApvmw1EwFZpCo0yJ7g7KVmIsT05b7/orGHxjS4MkTbXW+p7Kn4o3d0HJj2cWc2A8G/MG0Fa9Cs14lrgenJV3UhcEdAZ16Kq/CZfOwkEe/ouhSyQ82v5KNRl9GrKJQy+vzQJ5HqErl1RlyM2hJRKJRK0ICUIlEoAQiUICGUJqWUoDpRKRCAUFCAhAUeK47wxDdTvyXNYdzq9S5MNueq1qtJ1ao4bCf/gVtmEa1oAFyACey8Sbyeom2/atL5OuPGEa8kOHwjWBoaP6teZg/spcO2CMtiRHaDKlebzGx9lXomXsvMF4PQ5Zk+n6rXhGLSRS2ytjm5PNBI00gt2uq+DIcw84dvccle4mAWPmLETKwcFW0A8ovPqrcVF9C7Ro03+UmL9N1JRIJE6j2VIvLXZTB6jRW6ThY7qy+CGbgcCIJVmncH0CyKL/NPOFp4d+vZbxdszZO9puRrFp2TGVLgOF3C8bclM07oqU5HVWae47C+GMIQpnNlVTVbMStXkjFXJ0V4t6JEJ2W080kK6dkCISoUgrpUiUKQKhCFFgUJlc+U9bJ4TKgmB6/RZ5X+romOyHC0cgM6kz26IafYn2UzjZVmEZyJHPTouRJKkjW7IawImNRMcrka+6bQcGB7p1cS2dQICtYakScx2BHaFjcaaWGAqyVfsSu+hvE6melUjWx5zGqwqZGUATsTtBWtgSCHNNsw+7LEcwteWk2BWcu6ZK+C+x8wrlNyzqZV6i9WiGamGWlh32I3WNSrR7bE62WngnhwlaxZRl/CmAARBIFpkA9Dv3U73ECfZUOk727q+wEkTyv3Wql4RBKNFk4ulDxsTfvC2Fm8UbLWncHX9Vj6yCeO6uuycb/AGLlKS26YpaIho7KJdWL2IzlsEIQtSCunBNTglgEIQgFCCEiVUmriTHY0NJKxXVS2qTstd1WFSexj3lxtb3lccu6o1RZa/IwvGmv7LJx1ZtQmNY/0jieKLG5GmGHUkSspj4cGtLS635jb2WU530WjHyFGqAST7fqqeKZmOZqrPpua8sqODGu+RxmLn81ttZ7ro6bMORla6zm2cNA4ahVTvolquzBw74N1ogixCyMbi2sfaHX5QCOh+qvUOI0y0BrH5p0EEDczOyKSQaNEPtdafCKbi0kaJmBwDHAPcCIAMZjf/IH/XRWsDVg5QIc/wCYDtr+61jtNlH8Is16If5NHQD11iVo0T5e1u8WWfmkQSQ503F4uTr2CvUniwnaR2W8GuVlHokcTsqdeuDDTub9glxmJyAnkNN52VPC5nnPGsa7CbrLLkt8Fv8AotGPVs1qbYaAolZCrldkFSozYiEIVyCBKgBCAEIQgBKgBCrLQRVrmLqu6C22it1GSFA+wj1hefPpnQjNrMkEEW+7j2WeMFHnA1MuE5QBEuj0v6rUewuIHI/c9FVxzTDmt/pNhoS6xasrT7LL4MShTa9zDU84IfrJsJtGx0VvguCDb5bE3nQalrgfp9CrmEwrWkCLvFm7EmzgOkEfUqlxrFZmFjH5GU/LmEjxHAXaLyIGgOqrrsnfRQx2FYXPbnDXtuGZZ66g6+myucGwokGWuLh5QZFwTpOvL7tzbCW6EztI15noYXT8PDcPTa8hxc6HOk3AzPbDepv9HchMpq+w10bra4LCKZuSQSOYAvG4MEfVSYCrMOOa4MCwcdpt39x1WfggPK4y1j7NsDvlgf3bk626qd1Y0zMgllyJgX0HOBb63V1LyyleC/RYQTF2gNAF4EmCZE9E6rVLXNbmlrWgm1zfYcuioN4myNWguzQA4OEakW7yocznumQ0QAOcWsfooc1VRCT8l2vXD3TJjeJvda3D6UA3m9rbfZVPAYUBpEEEG5mdDtO38LWYAAtsGNuXKWykpdUh7jAVdOe6U1d6RkwQlCFIIEIQgBKEBCAEoCROVWCnia3lcGOFtTMwo6TABDySeayeIMawktcfOSYnRTYYvd56lmmMskS/W0ajb6rxnlcslNf5R1cajslrPyBxdo6QI1jqq+HqF4kWG19Y/wBnuoMY91RzA2BLgJ1EG8mBylW6dhebiIMDMdZ56KqjLk60TaopOqvY5z2gWmNLQ02bPf3lYHEsS57iDSfHmMwA2SATvPQLpyy4i5meoG45CYn02lNfTA1AmO/qr8HWxyXwcMa+UiaNWxOlJ176LsKGWoxpAIJDDBEOaMkCWm4N/cqQVWbNH/j9+6mbXE6RGlh+m9lKh9kOV+DPpNqtcC1hiHEAvECYiJM+11bGFe8nM6JMwJJ7EKz+IFu52NkuedItm3PmFwT0UxxJeSHIjoYNrIAtmka3P+M2It9ytLD4UWzaiexvyUNAXEwWjUHYzOafvRaNKJ7yQPvuFvjgkUk2T0WRMb/x+ymeUgEJF2QiZyYiUBEJVqVBCEICulQhACEIQChR4l+VjjEwFIEqrJXFoJ0znjg85zg5oiPTaFLxBmemfNBnMLxA0j6e6uYljmkuEweW3MLFxNUu8rjliwcLgjYOn5SvMlh4dHQpchwIY0ToTf029yE2viuu0Eg87W++So4+q+zHAZByEF3XoZTKYDmwHROk/mjaQop3Rfj5H18dHmm+oEzvInlsPUqhieIPJsNdb9fv6pMTRMkGRKgfSP3+iimTxFPEqk6xP3CnpYt5IzHTf9lWFHX12VvDUDOs/oJ1UqLDov4SfncbOGk7HY+s36LTot0jQxZ20zN1Tw9EDleAbRoffTVaeFpT6R9wtIxZmy2KM5xq05SBF7QYBFj98leYIguGg9e1vuyrmpkbJm+jRqSntdmOt9wdwtOkyqVllnMlPCbTbZPXXBVEylsEIQrkAhCEBAhCEAIQhAKEqQJUALOxXB2PdmksP9sX7rRQqSjGSqSJUmtGTj+Dh7PMZLdDlgjvC5F7zTOVw+U2Xoq5XjwLHOLWB9i4A78wuP1ONQSlE6MMuT4sxRUa6xJ+vtKdiXNsXQ0cyYmOp1Kx+LYpzvCygNZV1IF2u/pK5j4l4W5ha9uYtdaCT5XbrCDt9nS8dHaux1Bph9ak3/KqwfurDOKYZrc5rsyj8zXZh/6yvN34XPT89niI5uH8qnQrOZ5WtcQ4ic1m6xN1tGvBm4nq2E+KcI93h03ue+8BtJ4B/wC54A6LYpcXeQCxgZItnMuzA3BAECABeTqvPOAfDrmue9z4c3KczWyI1Ij2XaYCu948NzQ8OBkaOY08+qq5u+tDgls3MNWFTK9wIy2mbG2q0qTSZMgyRHQLOoMbkaym6IgFpF4Gq1cMw5jawiFaCbdGc+kWglQhd5yghCFIBCEICBCEIAQhAQDkIQgBCEIBQsXitPz6A5hqtoLK43YNd1hYZ43BmmF1I42vILqORpyedhO51XN1cc+vLKvlgkFjW/IdA7qul45iDTPihslgj0OpWVihnAxDXMYHOZMiS6IPljsvMSo9Ndo5PDYB9KvmrfKySCTc8oCvHDtxD2ZCMpMOi3ukx1T8Q58TlkkPJVhtMtYDTHkFyWnzW3W1t72Zulo1WYp9BzGtkRYBwk1DzJHbZdbgHNZLqrcj6oBcW3A7nZc5wt7yxr6rc7GwWCPPC6PDYhlct8N1pBc0gggDa/VV+ysjXw9OQ51nE/LHLZauEZDQCs6jTOew8oH2Fr0xZdeCPdnLlfQ5CELrMAQhCAAhKEICuhCEAJQkShAKhCEAIQhAKFR40wGmSdG3+ivLO4pimhpYfzWWeSuLsvjTclRyeJLHeRwJ8bQchusPiWvhuGSnTaI3z7ADquid4bRJuadx0lctxqqXuLqghseVv7heWlbPSvoxH1WuBY0hg2B1PqrHBsK8FznEtY2QQDZx7dVC3BguDnaRIBF1JR4u9jy3IHNJ+Q2PdaeKRTydJguKMfDKjCx8Q0tuOi6TDYUNpQyC4ABx3cVgcIo02uLnQx7rgOPygroOF4Q5i46bQdeqqt9CRtYOmWhgvMCVrBUMA07+ivrvwRqJw5X2CEIW5mCEIQCoQhAV5QkSoATk1KEAqEIQAlCAFS4hjQwa3KhulZMYuTpDeIY4MEDVcdieIzUGY6lN4xxWJuuOxPEpeDOhH6rizScj1MGFRXZ32KYC05Il3v0XP8Ta1rGuqEZ5GVvIdVs03ZaQczzOIkLC4hg3vAeDLwLjYjl0XKho5nHvfmDpOto0Wrgy0ZHVwCTyFx1Kr1C1jTu4nQ6Dsp8I5lazZa/2V3og6GjgW1nAseHNMTzHRdNSoua5racgDXlCwqPDzSpNFPXUuGpK6PhmdrB4hlx6KI9spN9G1hRZWFHSFgpF6cFUUjz5u2CEIVyAQhCAVCRCArpQkQhIqUJAgIQOQEKHE4gMChuiUrfQmMxQYLrhuO8SmZKu8Y4lrdcDxbiEkiVzTly6PQwYuPbKnEcWXEwVlOqc05z5UNQLI6rPX+GZTRYRoWj9Fj8axMtc2jt8xH7KX4Wq58Kz+0QfRR4qkGEuc6GHbeVzvZjX7M5Si5ziWEZh/VoQtzhGFZkc2mRmOpm6yX4xhe4Dyg7q1wrhTnVM2Y5G3sbuVmDp/h/D1W5nPccoMNaTI7rr8A8uALwNSAf4XOYKs8ODGiQeew7rp6Lg1obGnX+FbEuzDL2aTUqioPkKVekmcUlTBCEKSAQhCAEIQgKqcE1KEJHSgJErnQJQgbXqhokrleMcRiTKu8Vxmq4PjGJcZE2XNklfR24MVdsqcV4mTMFc1VeSVbrzuqLyFmddDZS4fDvqPaymJc8gAd/2TSvUP/zj4ZyD8TVb5njyA/kbz7lTGPJ0UyTUY2aOB4AMLh2tBJMDN1J1Kx+IYV7ydSIsF2PEcV841DVy4x8PggGVTLCPKzLFKTVs46tw7K8OqNLb6HRatLxGkGnYaADQ912pp0qoDHtBDtiFi4jg5wzwA4uY4y2b5eizlFx+zRST+jY4U57MpeMzjExA/VauIdoAIJjT3VPh1TPdws2IK0MO0PfPJaYVfRhN07ZewjIbdWEgSyu5Kjjk7dghEoUkAhCEAIQhAVAnJqUISOWdxDFbBWcTVgLneI14lZZJUqNsMLdsyuK4rquXxT5K0cdiJKzKjZXPs9GKpGdiws5zFq4kLPp0S97WN1eQ0ephCTo/gj4ZOJeKlQf8Kmd/+o4bdgvV8ViG02ZW7BVeH02YagxjYAa0DvAXP1+I56hE6LW1FUtnG08srekTvxALXl265evV88rb8MkOC5vHsLXSs2rSN4Km0jf4djc1QAH5Qurx7c9K0SI1XD8CYWjM7Vxt0C7fCPlkdFWMbtfIyqqa8FfB1WtaWxGULMw3FPOcptJU/F8U2lRe82JBXF8LruJBlUg2mXxQUk2z1PAYuRcrSBXH8OxJAC6rCvloK7YSs4c+NRdomQhC0OcWUSkQgFlCRCAqhKEIUAzsZqVynFXmdUIXPk2d2A53EaquEIWZ1eCji074dH/M0+5PshCjyTLR6Xxiocmuy5j4fM1HyhCtLZjj9jN2oFUrUm8h9EIVfJCK9H5x3XW4TbskQrx2Wy+1HJ/Hrz4TROrhPW6weDpULJbNcPtOwwey7LB/IEIXVjOP1OkToQhbHECEIQAhCEB//9k=", DateAdded=DateTime.Parse("2021-10-19") }
            };
            foreach (Menu m in menus)
            {
                context.Menu.Add(m);
            }
            context.SaveChanges();

            var orders = new Order[]
            {
            new Order{FirstName="Harry", LastName="Potter", Email="harry.potter@gmail.com", Flavor="Chocolate", Quantity=3, City="London", Street="Diagon", HouseNum=25, TemperatureId=0, TimeOrdered=DateTime.Parse("2021-10-01") },
            new Order{FirstName="Ron", LastName="Weasley", Email="ron.weasley@gmail.com", Flavor="Vanila", Quantity=1, City="Hogsmid", Street="Magic", HouseNum=1, TemperatureId=0, TimeOrdered=DateTime.Parse("2021-10-01") },
             new Order{FirstName="Hermione", LastName="Granger", Email="hermione.granger@gmail.com", Flavor="Strawberry", Quantity=1, City="Hogsmid", Street="Magic", HouseNum=1, TemperatureId=0, TimeOrdered=DateTime.Parse("2021-10-01") }
           };
            foreach (Order o in orders)
            {
                context.Order.Add(o);
            }
            context.SaveChanges();
        }
    }
}