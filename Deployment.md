-------------------

[TOC]

## 基础环境配置

> 域名和服务器请先自行购买

1. 基于 **云服务器ECS** 创建一个应用实例，选择系统镜像为 **Ubuntu 16.04**，在本机通过 **SSH** 进行远程连接，并进行相关配置

```bash
ssh root@yourhostip

...

sudo apt-get update
sudo apt-get upgrade
sudo apt-get autoremove
```

2. 安装并配置 Nginx

```bash
sudo apt-get install nginx
sudo service nginx start

sudo gedit /etc/nginx/sites-available/default
```

配置 default 文件，在文件末尾配置如下节点信息

```bash
# Virtual Host configuration for example.com
#
# You can move that to a different file under sites-available/ and symlink that
# to sites-enabled/ to enable it.
#
server {
	listen        80;
    # 网站文件的目标位置
	root /home/hippie/website/wwwroot;
    # 网站域名
	server_name your website name;
    	location / {
        	proxy_pass         http://localhost:5000;
        	proxy_http_version 1.1;
        	proxy_set_header   Upgrade $http_upgrade;
        	proxy_set_header   Connection keep-alive;
        	proxy_set_header   Host $host;
        	proxy_cache_bypass $http_upgrade;
        	proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        	proxy_set_header   X-Forwarded-Proto $scheme;
    }
}
```

检测配置并更新
```
sudo nginx -t
sudo nginx -s reload
```

3. 安装 DotNetCore

请参考官网最新安装说明：[.NetCore Download](https://www.microsoft.com/net/download)

## 部署流程

打开 **VisualStudio2017** 右键要发布的项目，点击 **publish**，并参考下图进行相关配置。

<center>
<img src="https://img2018.cnblogs.com/blog/749711/201810/749711-20181016181322591-598229590.png" width="80%" height="80%" />
<br/>
</center>

<center>
<img src="https://img2018.cnblogs.com/blog/749711/201810/749711-20181016181342214-1666292643.png" width="80%" height="80%" />
<br/>
</center>

点击 **Save** 按钮并执行发布操作。然后将 publish 文件夹上传至服务器相应位置，上传成功后执行

```bash
dotnet run app.dll
```

此处是关于 DotNetCore 部署的入门介绍，关于 HTTPS 的相关配置可以参考博客园的相应文章。

## 彩蛋

安利一个我的个人图片网站，图片资源来自于必应，感兴趣的小伙伴欢迎体验：

<center style="font-size:20px">
<a href="http://www.hippiezhou.fun">Pay Attention
</center>

## 相关参考

- [Host ASP.NET Core on Linux with Nginx](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-2.1&tabs=aspnetcore2x)