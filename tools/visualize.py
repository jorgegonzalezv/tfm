import os
import matplotlib.pyplot as plt
import matplotlib.patches as patches
import pandas as pd

DIR = os.path.join("/Users","jorge", "prueba-crest", "dataset")

SUPERSIZE = 4 # screenshot.cs

# read annotations
columns = ["filename", "xmax", "ymax", "xmin", "ymin", "label"]
labels = pd.read_csv(os.path.join(DIR, "debug.txt"), header=None)
labels.columns = columns

for imagepath in sorted(os.listdir(DIR)):
    if imagepath.split(".")[-1] != "png":
        continue

    # if int(imagepath.split("screenshot")[-1].split(".")[0]) != 13:
    #     continue

    im_path = os.path.join(DIR, imagepath)
    im = plt.imread(im_path)
    im_h, im_w, _ = im.shape

    rows = labels[labels.filename == int(imagepath.split("screenshot")[-1].split(".")[0])] 

    # display im in subplot figure
    fig, ax = plt.subplots()
    ax.imshow(im)
    plt.title(imagepath)
    for _, row in rows.iterrows():
        # flip vertical axis
        # xmax, ymax, xmin, ymin = row[1:5] * SUPERSIZE
        xmin, ymin, xmax, ymax = row[1:5] * SUPERSIZE
        # print(xmin, ymin, xmax, ymax)
        ymax_ = im_h - ymax
        ymax = im_h - ymin # true ymax
        ymin = ymax_
        # add bbox
        w = abs(xmax - xmin) 
        h = abs(ymax - ymin)

        padding = 0
        originX = min(xmin, xmax) - padding # min(row[1], row[3]) - padding 
        originY = min(ymin, ymax) - padding # min(row[2], row[4]) - padding

        print(imagepath,": ", (originX, originY), w + 2 * padding, h + 2 * padding)
        rect = patches.Rectangle((originX, originY), w + 2 * padding, h + 2 * padding, linewidth=1, edgecolor='r', facecolor='none')
        ax.add_patch(rect)
    
    plt.waitforbuttonpress(0)
    plt.close()
