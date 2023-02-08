Imports System.IO
Imports System.Net
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports Leaf.xNet
Imports Microsoft.VisualBasic.CompilerServices
Module Module1
	Public sessionid, target, id, email, sessionidtarget, username, mode, typeswap, name, msgbx, msg, bio, name2, ChocieProxies As String
	Public host As List(Of String) = New List(Of String)(New String() {"i.instagram.com", "b.i.instagram.com"})
	Public settings As List(Of String) = New List(Of String)()
	Public banner As List(Of String) = New List(Of String)()
	Public bl As StringBuilder = New StringBuilder()
	Public Threads, Errors, Done, swti As Integer
	Public ProxyList As String()
	Public stoop As Boolean
	Public Sub Main()
		Try
			Module1.ProxyList = File.ReadAllLines("Proxies.txt")
		Catch ex As Exception
			MsgBox("Proxies.txt Not Found", MsgBoxStyle.Critical, "Null Swap")
			ProjectData.EndApp()
		End Try
		Try
			Module1.settings = File.ReadAllLines("settings.txt").ToList()
			Module1.name = Regex.Match(Module1.settings(0), "Name:{(.*?)}").Groups(1).Value
			Module1.msgbx = Regex.Match(Module1.settings(1), "MsgBox:{(.*?)}").Groups(1).Value
			Module1.msg = Regex.Match(Module1.settings(2), "msg:{(.*?)}").Groups(1).Value
			Module1.bio = Regex.Match(Module1.settings(3), "Bio:{(.*?)}").Groups(1).Value
			Module1.banner = File.ReadAllLines("banner.txt").ToList()
			Console.ForegroundColor = ConsoleColor.Gray
			Console.WriteLine()
			Try
				For Each arg As String In Module1.banner
					Console.WriteLine(String.Format("  {0}", arg))
				Next
			Finally
				Dim enumerator As List(Of String).Enumerator
				CType(enumerator, IDisposable).Dispose()
			End Try
		Catch ex As Exception
			Dim flag As Boolean = ex.Message.Contains("settings.txt")
			If flag Then
				Using streamWriter As StreamWriter = New StreamWriter("settings.txt", False)
					Dim streamWriter2 As StreamWriter = streamWriter
					streamWriter2.WriteLine("Name:{Null}")
					streamWriter2.WriteLine("MsgBox:{Swapped}")
					streamWriter2.WriteLine("msg:{Swapped Successfully}")
					streamWriter2.WriteLine("Bio:{Swapped by @pkv}")
					streamWriter2.Dispose()
					streamWriter2.Close()
				End Using
				Module1.settings = File.ReadAllLines("settings.txt").ToList()
				Module1.name = Regex.Match(Module1.settings(0), "Name:{(.*?)}").Groups(1).Value
				Module1.msgbx = Regex.Match(Module1.settings(1), "MsgBox:{(.*?)}").Groups(1).Value
				Module1.msg = Regex.Match(Module1.settings(2), "msg:{(.*?)}").Groups(1).Value
				Module1.bio = Regex.Match(Module1.settings(3), "Bio:{(.*?)}").Groups(1).Value
			End If
		End Try
		Console.Title = String.Format("{0} Swap", Module1.name)
		Module1.line("!", "[1] Nothing | [2] Http\s | [3] Socks4 | [4] Socks5", True)
		Console.ForegroundColor = ConsoleColor.White
		Console.WriteLine()
		Module1.line("!", "Chocie Mode Proxies : ", True)
		Console.ForegroundColor = ConsoleColor.White
		Module1.ChocieProxies = Console.ReadLine()
		Console.WriteLine()
		Module1.line("!", "Enter Sessionid : ", True)
		Console.ForegroundColor = ConsoleColor.Black
		Module1.sessionid = Console.ReadLine()
		Console.WriteLine()
		Dim flag2 As Boolean = Module1.get_info(Module1.sessionid)
		If flag2 Then
			Dim flag3 As Boolean = Operators.CompareString(Module1.email, "", False) = 0
			If flag3 Then
				Module1.email = Module1.RandomLetterAndNumber(15) + "@gmail.com"
			End If
			Console.ForegroundColor = ConsoleColor.Red
			Console.Write(" < ")
			Console.ForegroundColor = ConsoleColor.White
			Console.Write("! ")
			Console.ForegroundColor = ConsoleColor.Red
			Console.Write("> ")
			Console.ForegroundColor = ConsoleColor.Red
			Console.Write(String.Format(" @{0}", Module1.username))
			Console.ForegroundColor = ConsoleColor.White
			Console.Write(" Logged In .")
			Console.WriteLine()
			Module1.line("!", "Grapped Info", False)
			Module1.line("!", "Checking Block ...", False)
			Dim flag4 As Boolean = Module1.Checkblock("https://i.instagram.com/api/v1/accounts/set_username/", String.Format("username={0}.block", Module1.username))
			If flag4 Then
				Module1.line("!", "Account is Working", False)
				Module1.line("!", "use auto swap? [y/n] : ", True)
				Module1.mode = Console.ReadLine().ToLower()
				Dim flag5 As Boolean = Operators.CompareString(Module1.mode, "n", False) = 0
				If flag5 Then
					Module1.typeswap = "Normal"
					Module1.line("!", "Enter Target : ", True)
					Module1.target = Console.ReadLine()
					Module1.line("!", "Enter Thread : ", True)
					Module1.Threads = Conversions.ToInteger(Console.ReadLine())
					Dim thread As Thread = New Thread(AddressOf Module1.ThreadSysyem)
					thread.Start()
				Else
					Dim flag6 As Boolean = Operators.CompareString(Module1.mode, "y", False) = 0
					If flag6 Then
						Module1.typeswap = "Auto"
						Module1.line("!", "Enter TargetSession: ", True)
						Module1.sessionidtarget = Console.ReadLine()
						Dim flag7 As Boolean = Module1.get_target(Module1.sessionidtarget)
						If flag7 Then
							Module1.line("!", String.Format("Enter Target : {0}", Module1.target), False)
							Module1.line("!", "Enter Thread : ", True)
							Module1.Threads = Conversions.ToInteger(Console.ReadLine())
							Dim thread2 As Thread = New Thread(AddressOf Module1.ThreadSysyem)
							thread2.Start()
							Dim thread3 As Thread = New Thread(AddressOf Module1.releaset)
							thread3.Start()
						Else
							Module1.line("x", "Bad Session", False)
							Console.ReadKey()
							ProjectData.EndApp()
						End If
					Else
						Module1.typeswap = "Normal"
						Module1.line("!", "Enter Target : ", True)
						Module1.target = Console.ReadLine()
						Module1.line("!", "Enter Thread : ", True)
						Module1.Threads = Conversions.ToInteger(Console.ReadLine())
						Dim thread4 As Thread = New Thread(AddressOf Module1.ThreadSysyem)
						thread4.Start()
					End If
				End If
			Else
				Module1.line("x", "Account is Blocked", False)
				Console.ReadKey()
				ProjectData.EndApp()
			End If
		End If
		Console.ReadLine()
	End Sub
	Public Sub counter()
		While True
			Dim flag As Boolean = Operators.CompareString(Module1.typeswap, "Normal", False) = 0
			If flag Then
				Console.Title = String.Format("Done:{0} Errors:{1}", Module1.Done, Module1.Errors)
			Else
				Dim flag2 As Boolean = Operators.CompareString(Module1.typeswap, "Auto", False) = 0
				If flag2 Then
					Module1.line("!", String.Format("Done:{0} , Errors:{1}{2}", Module1.Done, Module1.Errors, vbCr), True)
				End If
			End If
			Thread.Sleep(5)
		End While
	End Sub
	Public Sub releaset()
		Dim flag As Boolean
		Do
			flag = (Module1.Done >= 2)
		Loop While Not flag
		Dim start As ThreadStart
		Dim threadStart As ThreadStart = Sub()
												 Module1.release(String.Format("https://{0}/api/v1/accounts/set_username/", Module1.host(New Random().[Next](Module1.host.Count))), String.Format("username={0}.null.swap", Module1.target), String.Format("{0}.null.swap", Module1.target))
										 End Sub
		Dim thread As Thread = New Thread(start)
		thread.Start()
	End Sub
	Public Sub line(icon As String, text As String, Optional newline As Boolean = False)
		Console.ForegroundColor = ConsoleColor.Red
		Console.Write(" < ")
		Console.ForegroundColor = ConsoleColor.White
		Console.Write(String.Format("{0} ", icon))
		Console.ForegroundColor = ConsoleColor.Red
		Console.Write(">  ")
		Console.ForegroundColor = ConsoleColor.White
		Console.Write(String.Format("{0}", text))
		If newline Then
			Console.ForegroundColor = ConsoleColor.Red
		Else
			Console.WriteLine()
		End If
	End Sub
	Public Sub sendreq(url As String, data As String, username As String)
		Try
			Dim httpRequest As HttpRequest = New HttpRequest()
			If Module1.ChocieProxies.Contains("1") Then
				httpRequest.Proxy = Nothing
			ElseIf Module1.ChocieProxies.Contains("2") Then
				httpRequest.Proxy = HttpProxyClient.Parse(Module1.ProxyList(New Random().[Next](0, Module1.ProxyList.Length - 1)))
			ElseIf Module1.ChocieProxies.Contains("3") Then
				httpRequest.Proxy = Socks4ProxyClient.Parse(Module1.ProxyList(New Random().[Next](0, Module1.ProxyList.Length - 1)))
			ElseIf Module1.ChocieProxies.Contains("4") Then
				httpRequest.Proxy = Socks5ProxyClient.Parse(Module1.ProxyList(New Random().[Next](0, Module1.ProxyList.Length - 1)))
			End If
			httpRequest.IgnoreProtocolErrors = True
			httpRequest.UseCookies = True
			httpRequest.KeepAlive = False
			httpRequest.UserAgent = "Instagram 187.0.0.32.120 Android (25/7.1.2; 240dpi; 720x1280; google; G011A; G011A; intel; en_US; 289692181)"
			httpRequest.AddHeader("Cookie", String.Format("ds_user_id={0};sessionid={1}", Module1.RandomLetterAndNumber(15), Module1.sessionid))
			httpRequest.KeepAlive = True
			Dim httpResponse As HttpResponse = httpRequest.Post(url, data, "application/x-www-form-urlencoded; charset=UTF-8")
			Dim text As String = httpResponse.ToString()
			Dim flag As Boolean = text.Contains(username)
			If flag Then
				Dim flag2 As Boolean = Not Module1.bl.ToString().Contains(username)
				If flag2 Then
					Module1.bl.Append(username)
					Module1.stoop = True
					Module1.senddiscord(username)
					Using streamWriter As StreamWriter = New StreamWriter(String.Format("{0}.txt", username), False)
						Dim streamWriter2 As StreamWriter = streamWriter
						streamWriter2.WriteLine(String.Format(" ~ Username:@{0}", username))
						streamWriter2.WriteLine(String.Format(" ~ Email:{0}", Module1.email))
						streamWriter2.WriteLine(String.Format(" ~ Sessionid:{0}", Module1.sessionid))
						streamWriter2.WriteLine(String.Format(" ~ Time : {0:D}", DateAndTime.Now))
						streamWriter2.Dispose()
						streamWriter2.Close()
					End Using
					Dim start As ThreadStart
					Dim threadStart As ThreadStart = Sub()
														 Module1.setnameandbio(Module1.sessionid)
													 End Sub
					start = threadStart
					Dim thread As Thread = New Thread(threadStart)
					thread.Start()
					Console.WriteLine()
					Console.ForegroundColor = ConsoleColor.Red
					Console.Write(" < ")
					Console.ForegroundColor = ConsoleColor.White
					Console.Write("✓ ")
					Console.ForegroundColor = ConsoleColor.Red
					Console.Write("> ")
					Console.ForegroundColor = ConsoleColor.Red
					Console.Write(String.Format(" @{0}", username))
					Console.ForegroundColor = ConsoleColor.White
					Console.Write(String.Format(" {0}", Module1.msg))
					Console.WriteLine()
					Interaction.MsgBox(String.Format("{0} :@{1}", Module1.msgbx, username), MsgBoxStyle.OkOnly, String.Format("{0} Swap", Module1.name))
				End If
			Else
				Dim flag3 As Boolean = text.Contains("wait") Or text.Contains("rate_limit_error") Or text.Contains("input")
				If flag3 Then
					Module1.Errors += 1
				Else
					Dim flag4 As Boolean = text.Contains("This username")
					If flag4 Then
						Module1.Done += 1
					Else
						Dim flag5 As Boolean = text.Contains("challenge_required") Or text.Contains("login_required") Or text.Contains("spam")
						If flag5 Then
							Module1.stoop = True
							Interaction.MsgBox("Blocked", MsgBoxStyle.OkOnly, Nothing)
							ProjectData.EndApp()
						End If
					End If
				End If
			End If
			httpRequest.Dispose()
			httpRequest.Close()
		Catch ex As Exception
		End Try
	End Sub
	Public Sub setnameandbio(sessionid As String)
		Module1.setbio(sessionid)
		Module1.setname(sessionid)
	End Sub
	Public Sub release(url As String, data As String, username As String)
		Try
			Dim httpRequest As HttpRequest = New HttpRequest()
			httpRequest.IgnoreProtocolErrors = True
			httpRequest.UseCookies = True
			httpRequest.KeepAlive = False
			httpRequest.UserAgent = "Instagram 187.0.0.32.120 Android (25/7.1.2; 240dpi; 720x1280; google; G011A; G011A; intel; en_US; 289692181)"
			httpRequest.AddHeader("Cookie", $"ds_user_id={Module1.RandomLetterAndNumber(15)}; sessionid={Module1.sessionidtarget}")
			httpRequest.KeepAlive = True
			Dim httpResponse As HttpResponse = httpRequest.Post(url, data, "application/x-www-form-urlencoded; charset=UTF-8")
			Dim text As String = httpResponse.ToString()
			Dim flag As Boolean = text.Contains("full_name")
			If flag Then
				Module1.line("$", String.Format("released :@{0}", username), True)
			Else
				Dim flag2 As Boolean = text.Contains("wait") Or text.Contains("rate_limit_error")
				If flag2 Then
					Module1.line("$", String.Format("Blocked IP :@{0}", username), True)
				Else
					Dim flag3 As Boolean = text.Contains("generic") Or text.Contains("input")
					If flag3 Then
						Module1.release(String.Format("https://{0}/api/v1/accounts/set_username/", Module1.host(New Random().[Next](Module1.host.Count))), String.Format("username={0}.null.swap", username), username)
					Else
						Dim flag4 As Boolean = text.Contains("This username")
						If flag4 Then
							Module1.release(String.Format("https://{0}/api/v1/accounts/set_username/", Module1.host(New Random().[Next](Module1.host.Count))), String.Format("username={0}.null.swap.v1", username), username)
						Else
							Dim flag5 As Boolean = text.Contains("challenge_required") Or text.Contains("login_required") Or text.Contains("spam")
							If flag5 Then
								Module1.stoop = True
								Module1.line("$", String.Format("Locked Num :@{0}", username), True)
							Else
								Console.WriteLine(text)
								Module1.stoop = True
							End If
						End If
					End If
				End If
			End If
			httpRequest.Dispose()
			httpRequest.Close()
		Catch ex As Exception
		End Try
	End Sub
	Public Function get_info(sessionid As String) As Boolean
		Dim httpWebRequest As HttpWebRequest = CType(WebRequest.Create("https://i.instagram.com/api/v1/accounts/current_user/?edit=true"), HttpWebRequest)
		httpWebRequest.Method = "GET"
		httpWebRequest.UserAgent = "Instagram 136.0.0.34.124 Android"
		httpWebRequest.Headers.Add("Cookie", "sessionid=" + sessionid)
		httpWebRequest.AutomaticDecompression = (DecompressionMethods.GZip Or DecompressionMethods.Deflate)
		Dim httpWebResponse As HttpWebResponse
		Try
			httpWebResponse = CType(httpWebRequest.GetResponse(), HttpWebResponse)
		Catch ex As WebException
			httpWebResponse = CType(ex.Response, HttpWebResponse)
		End Try
		Dim streamReader As StreamReader = New StreamReader(httpWebResponse.GetResponseStream())
		Dim text As String = streamReader.ReadToEnd()
		Dim flag As Boolean = text.Contains("ok")
		Dim result As Boolean
		If flag Then
			Module1.email = Regex.Match(text, "email"":""(.*?)"",").Groups(1).Value
			Module1.id = Regex.Match(text, "{""pk"":(.*?),").Groups(1).Value
			Module1.username = Regex.Match(text, "username"":""(.*?)"",").Groups(1).Value
			result = True
		Else
			Console.ForegroundColor = ConsoleColor.White
			Console.WriteLine(text)
			result = False
		End If
		Return result
	End Function
	Public Function get_target(sessionid As String) As Boolean
		Dim httpWebRequest As HttpWebRequest = CType(WebRequest.Create("https://i.instagram.com/api/v1/accounts/current_user/?edit=true"), HttpWebRequest)
		httpWebRequest.Method = "GET"
		httpWebRequest.UserAgent = "Instagram 136.0.0.34.124 Android"
		httpWebRequest.Headers.Add("Cookie", "sessionid=" + sessionid)
		httpWebRequest.AutomaticDecompression = (DecompressionMethods.GZip Or DecompressionMethods.Deflate)
		Dim httpWebResponse As HttpWebResponse
		Try
			httpWebResponse = CType(httpWebRequest.GetResponse(), HttpWebResponse)
		Catch ex As WebException
			httpWebResponse = CType(ex.Response, HttpWebResponse)
		End Try
		Dim streamReader As StreamReader = New StreamReader(httpWebResponse.GetResponseStream())
		Dim text As String = streamReader.ReadToEnd()
		Dim flag As Boolean = text.Contains("ok")
		Dim result As Boolean
		If flag Then
			Module1.target = Regex.Match(text, "username"":""(.*?)"",").Groups(1).Value
			result = True
		Else
			Console.WriteLine(text)
			result = False
		End If
		Return result
	End Function
	Public Function Checkblock(url As String, data As String) As Boolean
		Dim bytes As Byte() = Encoding.UTF8.GetBytes(data)
		Dim httpWebRequest As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
		httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip
		httpWebRequest.Method = "POST"
		httpWebRequest.Proxy = Nothing
		httpWebRequest.Headers.Add("Cookie", "sessionid=" + Module1.sessionid)
		httpWebRequest.UserAgent = "Instagram 187.0.0.32.120 Android (25/7.1.2; 240dpi; 720x1280; google; G011A; G011A; intel; en_US; 289692181)"
		httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
		httpWebRequest.ContentLength = CLng(bytes.Length)
		httpWebRequest.KeepAlive = True
		httpWebRequest.UnsafeAuthenticatedConnectionSharing = True
		httpWebRequest.PreAuthenticate = True
		httpWebRequest.Pipelined = True
		httpWebRequest.ContentLength = CLng(bytes.Length)
		Dim requestStream As Stream = httpWebRequest.GetRequestStream()
		requestStream.Write(bytes, 0, bytes.Length)
		requestStream.Close()
		Dim httpWebResponse As HttpWebResponse
		Try
			httpWebResponse = CType(httpWebRequest.GetResponse(), HttpWebResponse)
		Catch ex As WebException
			httpWebResponse = CType(ex.Response, HttpWebResponse)
		End Try
		Dim flag As Boolean = httpWebResponse IsNot Nothing
		Dim result As Boolean
		If flag Then
			Dim streamReader As StreamReader = New StreamReader(httpWebResponse.GetResponseStream())
			Dim text As String = streamReader.ReadToEnd()
			Dim flag2 As Boolean = text.Contains("full_name")
			If flag2 Then
				result = True
			Else
				Console.WriteLine(text)
				result = False
			End If
		Else
			result = False
		End If
		Return result
	End Function
	Public Function RandomLetterAndNumber(MaxSize As Integer) As String
		Try
			Dim array As Char() = New Char(61) {}
			array = "abcdefghijklmnopqrstuvwxyz123456789".ToCharArray()
			Dim array2 As Byte() = New Byte(MaxSize - 1 + 1 - 1 + 1 - 1) {}
			Dim rngcryptoServiceProvider As RNGCryptoServiceProvider = New RNGCryptoServiceProvider()
			rngcryptoServiceProvider.GetNonZeroBytes(array2)
			Dim stringBuilder As StringBuilder = New StringBuilder(MaxSize)
			Dim num As Integer = array2.Length - 1
			For i As Integer = 0 To num
				stringBuilder.Append(array(CInt(array2(i)) Mod array.Length))
			Next
			Return stringBuilder.ToString()
		Catch ex As Exception
			Module1.RandomLetterAndNumber(MaxSize)
		End Try
		Return Conversions.ToString(False)
	End Function
	Public Sub setbio(sessionid As String)
		Try
			Dim utf8Encoding As UTF8Encoding = New UTF8Encoding()
			Dim bytes As Byte() = utf8Encoding.GetBytes(String.Format("raw_text={0}", Module1.bio))
			Dim httpWebRequest As HttpWebRequest = CType(WebRequest.Create("https://i.instagram.com/api/v1/accounts/set_biography/"), HttpWebRequest)
			httpWebRequest.Method = "POST"
			httpWebRequest.UserAgent = "Instagram 100.1.0.29.135 Android (25/7.1.2; 192dpi; 720x1280; google; G011A; G011A; qcom; en_US; 262886984)"
			httpWebRequest.Headers.Add("Cookie", "sessionid=" + sessionid)
			httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
			httpWebRequest.AutomaticDecompression = (DecompressionMethods.GZip Or DecompressionMethods.Deflate)
			httpWebRequest.Host = "i.instagram.com"
			httpWebRequest.ContentLength = CLng(bytes.Length)
			Dim requestStream As Stream = httpWebRequest.GetRequestStream()
			requestStream.Write(bytes, 0, bytes.Length)
			requestStream.Dispose()
			requestStream.Close()
			Dim httpWebResponse As HttpWebResponse
			Try
				httpWebResponse = CType(httpWebRequest.GetResponse(), HttpWebResponse)
				Dim flag As Boolean = httpWebResponse Is Nothing
				Dim flag2 As Boolean = flag
				Dim flag3 As Boolean = flag2
				If flag3 Then
					Return
				End If
			Catch ex As WebException
				httpWebResponse = CType(ex.Response, HttpWebResponse)
				Dim flag4 As Boolean = httpWebResponse Is Nothing
				Dim flag5 As Boolean = flag4
				Dim flag6 As Boolean = flag5
				If flag6 Then
					Return
				End If
			End Try
			Dim streamReader As StreamReader = New StreamReader(httpWebResponse.GetResponseStream())
			Dim text As String = streamReader.ReadToEnd()
			streamReader.Dispose()
			streamReader.Close()
		Catch ex2 As WebException
		End Try
	End Sub
	Public Sub setname(sessionid As String)
		Dim utf8Encoding As UTF8Encoding = New UTF8Encoding()
		Dim bytes As Byte() = utf8Encoding.GetBytes("first_name=Null Swap")
		Dim httpWebRequest As HttpWebRequest = CType(WebRequest.Create("https://i.instagram.com/api/v1/accounts/set_phone_and_name/"), HttpWebRequest)
		httpWebRequest.Method = "POST"
		httpWebRequest.UserAgent = "Instagram 100.1.0.29.135 Android (25/7.1.2; 192dpi; 720x1280; google; G011A; G011A; qcom; en_US; 262886984)"
		httpWebRequest.Headers.Add("Cookie", "sessionid=" + sessionid)
		httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
		httpWebRequest.AutomaticDecompression = (DecompressionMethods.GZip Or DecompressionMethods.Deflate)
		httpWebRequest.Host = "i.instagram.com"
		httpWebRequest.ContentLength = CLng(bytes.Length)
		Dim requestStream As Stream = httpWebRequest.GetRequestStream()
		requestStream.Write(bytes, 0, bytes.Length)
		requestStream.Dispose()
		requestStream.Close()
		Dim httpWebResponse As HttpWebResponse
		Try
			httpWebResponse = CType(httpWebRequest.GetResponse(), HttpWebResponse)
			Dim flag As Boolean = httpWebResponse Is Nothing
			Dim flag2 As Boolean = flag
			Dim flag3 As Boolean = flag2
			If flag3 Then
				Return
			End If
		Catch ex As WebException
			httpWebResponse = CType(ex.Response, HttpWebResponse)
			Dim flag4 As Boolean = httpWebResponse Is Nothing
			Dim flag5 As Boolean = flag4
			Dim flag6 As Boolean = flag5
			If flag6 Then
				Return
			End If
		End Try
		Dim streamReader As StreamReader = New StreamReader(httpWebResponse.GetResponseStream())
		Dim text As String = streamReader.ReadToEnd()
		streamReader.Dispose()
		streamReader.Close()
	End Sub
	Public Function rndcolor() As String
		Dim random As Random = New Random()
		Dim str As String = String.Format("{0:X6}", random.[Next](0, 1000000))
		Dim value As Integer = Conversions.ToInteger("&H" + str)
		Return Conversions.ToString(value)
	End Function
	Public Sub senddiscord(Target As String)
		Try
			Dim utf8Encoding As UTF8Encoding = New UTF8Encoding()
			Dim bytes As Byte() = utf8Encoding.GetBytes(String.Concat(New String() {String.Concat(New String() {"{""content"":"""",""embeds"": [{""title"": ""New Swap"",""color"": """ + Module1.rndcolor() + """,""fields"": [{""name"": ""- Done Swapped : [@", Target, "]"",""value"": ""- After Attempts : [", Module1.Done.ToString(), "]\n- By : [ ", Module1.name2, "]"",""inline"": false}],""thumbnail"": {""url"": ""https://cdn.discordapp.com/attachments/940714745357557800/940715722408075264/giphy.gif""}}]}"})}))
			Dim APIREQUEST As HttpWebRequest = CType(WebRequest.Create("WebHook"), HttpWebRequest)
			APIREQUEST.Method = "POST"
			APIREQUEST.Proxy = Nothing
			APIREQUEST.Host = "discord.com"
			APIREQUEST.KeepAlive = True
			APIREQUEST.Headers.Add("sec-ch-ua: ""Google Chrome"";v=""87"", "" Not;A Brand"";v=""99"", ""Chromium"";v=""87""")
			APIREQUEST.Accept = "application/json"
			APIREQUEST.Headers.Add("Accept-Language: en")
			APIREQUEST.Headers.Add("sec-ch-ua-mobile: ?0")
			APIREQUEST.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36"
			APIREQUEST.ContentType = "application/json"
			APIREQUEST.Headers.Add("Origin: https://discohook.org")
			APIREQUEST.Headers.Add("Sec-Fetch-Site: cross-site")
			APIREQUEST.Headers.Add("Sec-Fetch-Mode: cors")
			APIREQUEST.Headers.Add("Sec-Fetch-Dest: empty")
			APIREQUEST.Referer = "https://discohook.org/"
			APIREQUEST.AutomaticDecompression = (DecompressionMethods.GZip Or DecompressionMethods.Deflate)
			APIREQUEST.ContentLength = CLng(bytes.Length)
			Dim Stream As Stream = APIREQUEST.GetRequestStream()
			Stream.Write(bytes, 0, bytes.Length)
			Stream.Dispose()
			Stream.Close()
			Dim StreamReader As StreamReader = New StreamReader(CType(APIREQUEST.GetResponse(), HttpWebResponse).GetResponseStream())
			Dim Response As String = StreamReader.ReadToEnd()
			StreamReader.Dispose()
			StreamReader.Close()
		Catch ex As WebException
			Dim str As String = New StreamReader(ex.Response.GetResponseStream()).ReadToEnd()
			Interaction.MsgBox("Error From Send Discord : " + str, MsgBoxStyle.Critical, "Null Swap")
		End Try
	End Sub
	Public Sub ThreadSysyem()
		Dim flag As Boolean = Operators.CompareString(Module1.mode, "n", False) = 0
		If flag Then
			Interaction.MsgBox(String.Format("Target :{0}{1}Ready?", Module1.target, vbCrLf), MsgBoxStyle.OkOnly, String.Format("{0} Swap", Module1.name))
			Dim thread As Thread = New Thread(AddressOf Module1.counter)
			thread.Start()
		Else
			Dim flag2 As Boolean = Operators.CompareString(Module1.mode, "y", False) = 0
			If flag2 Then
				Module1.line("!", "Press Any Key To Start ..", True)
				Console.ReadKey()
				Console.WriteLine()
				Dim thread2 As Thread = New Thread(AddressOf Module1.counter)
				thread2.Start()
			Else
				Interaction.MsgBox(String.Format("Target :{0}{1}Ready?", Module1.target, vbCrLf), MsgBoxStyle.OkOnly, String.Format("{0} Swap", Module1.name))
				Dim thread3 As Thread = New Thread(AddressOf Module1.counter)
				thread3.Start()
			End If
		End If
		Dim threads As Integer = Module1.Threads
		Dim i As Integer = 0
		While i <= threads
			Try
				Dim start As ParameterizedThreadStart
				Dim parameterizedThreadStart As ParameterizedThreadStart = Sub(a As Object)
																				   Module1.Run()
																			   End Sub
				start = parameterizedThreadStart
				Dim thread4 As Thread = New Thread(start)
				thread4.Start()
				thread4.Priority = ThreadPriority.Highest
			Catch ex As Exception
			End Try
IL_152:
			i += 1
			Continue While
			GoTo IL_152
		End While
	End Sub
	Public Sub Run()
		While Not Module1.stoop
			Try
				Module1.swti += 1
				Dim flag As Boolean = Module1.swti >= 2
				If flag Then
					Module1.swti = 0
				End If
				Dim flag2 As Boolean = Module1.swti = 1
				If flag2 Then
					Module1.sendreq(String.Format("https://{0}/api/v1/accounts/set_username/", Module1.host(New Random().[Next](Module1.host.Count))), String.Format("username={0}", Module1.target), Module1.target)
				Else
					Dim flag3 As Boolean = Module1.swti = 2
					If flag3 Then
						Module1.sendreq(String.Format("https://{0}/api/v1/accounts/edit_profile/", Module1.host(New Random().[Next](Module1.host.Count))), String.Concat(New String() {"signed_body=SIGNATURE.{""external_url"":"""",""phone_number"":"""",""_csrftoken"":""", Module1.RandomLetterAndNumber(20), """,""username"":""", Module1.target, """,""first_name"":"""",""_uid"":""", Module1.id, """,""device_id"":""", Guid.NewGuid().ToString(), """,""biography"":"""",""_uuid"":""", Guid.NewGuid().ToString(), """,""email"":""", Module1.email, """}"}), Module1.target)
					End If
				End If
			Catch ex As Exception
			End Try
		End While
	End Sub
End Module
