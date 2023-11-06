#!/bin/bash

# validate pretrained COCO on tfm dataset over SeaDroneSea validation dataset.

rm datasets/seadronesee/labels/val.cache 2> /dev/null
mv datasets/seadronesee/labels/val_coco datasets/seadronesee/labels/val

LIBPATH=lib/yolov5-master

python $LIBPATH/val.py \
    --data datasets/seadronesee/data_coco.yaml \
    --name coco_pretrained_sds \
    --weights $LIBPATH/yolov5s.pt \
    --save-txt \
    --save-json \
    --save-conf

echo ".... renaming....."
mv datasets/seadronesee/labels/val datasets/seadronesee/labels/val_coco
echo "ENDED WITH SUCCESS!"
