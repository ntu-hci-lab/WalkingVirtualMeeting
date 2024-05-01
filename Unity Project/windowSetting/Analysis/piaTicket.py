import requests
from bs4 import BeautifulSoup
import hashlib
import time
import json
from datetime import datetime

def get_page_content(url, name):
    cnt = 0
    response = requests.get(url)
    while response.status_code != 200 and cnt < 10:
        print(name + " .")
        response = requests.get(url)        
        cnt += 1
        time.sleep(1)
    return response.text if response else None

def extract_text_content(html_content):
    soup = BeautifulSoup(html_content, 'html.parser')
    text_content = soup.get_text(separator='\n', strip=True)
    return text_content

def extract_target_element(html_content):
    soup = BeautifulSoup(html_content, 'html.parser')
    target_element = soup.find('th', class_='w83')
    # print(target_element.text.strip())
    return target_element.text.strip() if target_element else None

def calculate_hash(content):
    return hashlib.md5(content.encode('utf-8')).hexdigest()

def check_website(website_info):
        previous_hash = website_info["previous_hash"]
        page_content = get_page_content(website_info['url'], website_info['name'])
        # print(page_content)

        if page_content:
            current_hash = calculate_hash(page_content)
            # print(current_hash, previous_hash)

            if current_hash != previous_hash:
                
                
                if previous_hash is not None:
                    # if "アクセスが集中しております" in page_content:
                        # print(page_content)
                        # print("アクセスが集中しております")
                        # requests.get("https://api.telegram.org/bot6516022736:AAHV-zCLQicLlz1sw2wk2tbFVEHYP7wS6dA/sendMessage?chat_id=-1001921365581&text=アクセスが集中しております")
                    if "Ｋｉｓ－Ｍｙ－Ｆｔ２" in page_content and "アクセスが集中しております" not in page_content:
                        current_time = datetime.now().strftime("%H:%M:%S")
                        print(f"{current_time}{website_info['name']} 有票了！")
                        requests.get("https://api.telegram.org/bot6516022736:AAHV-zCLQicLlz1sw2wk2tbFVEHYP7wS6dA/sendMessage?chat_id=-1001921365581&text=%s" % (website_info['name'] + " " + extract_target_element(page_content)+"\n"+website_info['url'].replace("&", "%26")))
                        website_info["previous_hash"] = current_hash
                        # print(page_content)
                else:
                    current_time = datetime.now().strftime("%H:%M:%S")
                    title = extract_target_element(page_content)                    
                    if title and title != "予定枚数終了":
                        requests.get("https://api.telegram.org/bot6516022736:AAHV-zCLQicLlz1sw2wk2tbFVEHYP7wS6dA/sendMessage?chat_id=-1001921365581&text=%s" % (website_info['name'] + " " + extract_target_element(page_content)+"\n"+website_info['url'].replace("&", "%26")))
                        print(f"{current_time}{website_info['name']} 有票了！")
                    if "Ｋｉｓ－Ｍｙ－Ｆｔ２" in page_content and "アクセスが集中しております" not in page_content:
                        website_info["previous_hash"] = current_hash
                    print(f"{current_time}{website_info['name']}")
                   
            else:
                title = extract_target_element(page_content)  
                current_time = datetime.now().strftime("%H:%M:%S")
                print(f"{current_time}{website_info['name']} {title}")

        

def main(config_file="./config.json"):
    with open(config_file, 'r', encoding="utf-8") as f:
        config = json.load(f)

    for website_info in config.get('websites', []):
        website_info['previous_hash'] = None
        website_info['previous_content'] = None
    try:
        while True:
            for website_info in config.get('websites', []):
                check_website(website_info)
                # time.sleep(1)
            time.sleep(3)
    except:
        requests.get("https://api.telegram.org/bot6516022736:AAHV-zCLQicLlz1sw2wk2tbFVEHYP7wS6dA/sendMessage?chat_id=-1001921365581&text=%s" % ("Error!!!"))


if __name__ == "__main__":
    main()